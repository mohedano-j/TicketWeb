import { useState } from "react";
import axios from "axios";
import { Observable } from "rxjs";

axios.defaults.baseURL = "/api/";
axios.defaults.headers.post["Content-Type"] = "application/json";
axios.defaults.withCredentials = true;

/*
Handles any HTTP errors that are encountered when calling a REST API.
*/
const handleHttpError = (observer: any, error: { response: { data: any }; message: string }) => {
  if (error?.response?.data) {
    observer.error(error.response.data);
  } else {
    var errorSa: any = { title: "An unhandled error occured", detail: error.message };
    observer.error(errorSa);
  }
};

/*
Creates an observable for a promise that returns a single object.
*/
const observableForOne = <T extends unknown>(promise: any, transformer: Function) => {
  const observable$ = new Observable<T>((observer: any) => {
    return promise
      .then((resp: { data: any }) => {
        const transformed = transformer(resp.data);
        observer.next(transformed);
        observer.complete();
      })
      .catch((error: { response: { data: any }; message: string }) => {
        handleHttpError(observer, error);
      });
  });
  return observable$;
};

/*
Creates an observable for a promise that returns a list of objects.
*/
const observableForMany = <T extends unknown>(promise: any, transformer: Function) => {
  const observable$ = new Observable<T>((observer: any) => {
    return promise
      .then((resp: { data: any }) => {
        const transformed = resp.data.map(transformer);
        observer.next(transformed);
        observer.complete();
      })
      .catch((error: { response: { data: any }; message: string }) => {
        handleHttpError(observer, error);
      });
  });
  return observable$;
};

/*
Creates an observable for a promise that returns a SearcResult<T> object.
*/
const observableForSearch = <T extends unknown>(promise: any, transformer: Function) => {
  const observable$ = new Observable<T>((observer: any) => {
    return promise
      .then((resp: { data: any }) => {
        const transformed = resp.data;
        transformed.payload = transformed.payload.map(transformer);
        observer.next(transformed);
        observer.complete();
      })
      .catch((error: { response: { data: any }; message: string }) => {
        handleHttpError(observer, error);
      });
  });
  return observable$;
};

/*
This represents a custom hook that can make calls over HTTP to a REST API.
This hook can track its status (e.g. "am I working/making a request") or an
an error that was encountered.
*/
export const useHttp = () => {
  const [working, setWorking] = useState<boolean>(false);
  const [error, setError] = useState<any | null>(null);

  /*
  Subscribes this hook to a given observer. This allows the useHttp hook to updates
  its state when any of the observable events have completed.
  */
  const subscribe = <T extends any>(observable: Observable<T>) => {
    observable.subscribe(
      (result: T) => {
        setWorking(false);
        setError(null);
      },
      (error: any) => {
        setWorking(false);
        setError(error);
        console.error(error);
      }
    );
  };

  /*
  Returns an observable for a GET request that returns a single object. (e.g. GET /things/1)
  */
  const get = <T extends unknown>(path: string, params: any = undefined, transformer: any) => {
    setWorking(true);
    setError(null);
    const promise = axios.get(path, { params: params });
    const observable = observableForOne<T>(promise, transformer);
    subscribe(observable);
    return observable;
  };

  /*
  Returns an observable for GET request that returns multiple objects. (e.g. GET /things?type=foo)
  */
  const getList = <T extends unknown>(path: string, params: any = undefined, transformer: any) => {
    setWorking(true);
    setError(null);
    const promise = axios.get(path, { params: params });
    const observable = observableForMany<T>(promise, transformer);
    subscribe(observable);
    return observable;
  };

  /*
  Returns an observable for a GET request that returns a SearchResult object.  (e.g. GET /things/search?type=foo)
  */
  const search = <T extends unknown>(path: string, params: any = undefined, transformer: any) => {
    setWorking(true);
    setError(null);
    const promise = axios.get(path, { params: params });
    const observable = observableForSearch<T>(promise, transformer);
    subscribe(observable);
    return observable;
  };

  /*
  Returns an observable for a POST request. Each POST request should return the newly created object.
  */
  const post = <T extends unknown>(path: string, obj: any, transformer: any) => {
    setWorking(true);
    setError(null);
    const promise = axios.post(path, obj);
    const observable = observableForOne<T>(promise, transformer);
    subscribe(observable);
    return observable;
  };

  /*
  Returns an observable for a PUT request. Each PUT request should return the newly edited object.
  */
  const put = <T extends unknown>(path: string, obj: any, transformer: any) => {
    setWorking(true);
    setError(null);
    const promise = axios.put(path, obj);
    const observable = observableForOne<T>(promise, transformer);
    subscribe(observable);
    return observable;
  };

  /*
  Returns an observable for a DELETE request. Each DELETE request should return the deleted object.
  */
  const delete_ = <T extends unknown>(path: string, obj: any, transformer: any) => {
    setWorking(true);
    setError(null);
    const observable = observableForOne<T>(axios.delete(path, { data: obj }), transformer);
    subscribe(observable);
    return observable;
  };

  return {
    working,
    error,
    get,
    getList,
    search,
    post,
    put,
    delete_,
  };
};
