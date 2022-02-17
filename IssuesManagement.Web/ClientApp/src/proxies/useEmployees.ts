import { EmployeeFromJSON, Employee } from "../models";
import { useHttp } from ".";
import { Observable } from "rxjs";

const path = "employees";

export type IEmployeeProxy = {
  working: boolean;
  error: any| null;
  getList: () => Observable<Employee[]>;
};

export const useEmployeesProxy = (): IEmployeeProxy => {
  const http = useHttp();

  const getList= (): Observable<Employee[]> => {
    return http.getList(path, null, EmployeeFromJSON);
  };


  return {
    working: http.working,
    error: http.error,
    getList,
  };
};
