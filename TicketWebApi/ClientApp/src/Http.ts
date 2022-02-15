import axios from "axios";

axios.defaults.baseURL = "/campuspoint/v1/";
axios.defaults.headers.post["Content-Type"] = "application/json";
axios.defaults.withCredentials = true;

const Http = () => {
  const get = (path: string, params: any = undefined) => {
    return axios.get(path, { params: params });
  };

  const post = (path: string, obj: any) => {
    return axios.post(path, obj);
  };

  const put = (path: string, obj: any) => {
    return axios.put(path, obj);
  };

  const delete_ = (path: string, obj: any) => {
    return axios.delete(path, { data: obj });
  };
  return { get, post, put, delete_ };
};

export const http = Http();
