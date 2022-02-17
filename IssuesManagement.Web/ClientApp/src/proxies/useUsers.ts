import { UserFromJSON, User } from "../models";
import { useHttp } from ".";
import { Observable } from "rxjs";

const path = "users";

export type IUsersProxy = {
  working: boolean;
  error: any | null;
  getList: () => Observable<User[]>;
};

export const useUsersProxy = (): IUsersProxy => {
  const http = useHttp();

  const getList = (): Observable<User[]> => {
    return http.getList(path, null, UserFromJSON);
  };

  return {
    working: http.working,
    error: http.error,
    getList,
  };
};
