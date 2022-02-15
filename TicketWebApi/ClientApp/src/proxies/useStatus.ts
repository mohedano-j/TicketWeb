import { StatusFromJSON, Status } from "../models";
import { useHttp } from ".";
import { Observable } from "rxjs";

const path = "Status";

export type IStatussProxy = {
  working: boolean;
  error: any| null;
  getList: () => Observable<Status[]>;
};

export const useStatusProxy = (): IStatussProxy => {
  const http = useHttp();

  const getList= (): Observable<Status[]> => {
    return http.getList(path, null, StatusFromJSON);
  };


  return {
    working: http.working,
    error: http.error,
    getList,
  };
};
