import { ProjectFromJSON, Project } from "../models";
import { useHttp } from ".";
import { Observable } from "rxjs";

const path = "Projects";

export type IProjectsProxy = {
  working: boolean;
  error: any| null;
  getList: () => Observable<Project[]>;
  add: (project: Project) => Observable<Project>;
  edit: (project: Project) => Observable<Project>;
  delete_: (project: Project) => Observable<Project>;
};

export const useProjectsProxy = (): IProjectsProxy => {
  const http = useHttp();

  const getList= (): Observable<Project[]> => {
    return http.getList(path, null, ProjectFromJSON);
  };

  const add = (project: Project) => {
    return http.post<Project>(path, project, ProjectFromJSON);
  };

  const edit = (project: Project) => {
    return http.put<Project>(path, project, ProjectFromJSON);
  };

  const delete_ = (project: Project) => {
    return http.delete_<Project>(path, project, ProjectFromJSON);
  };

  return {
    working: http.working,
    error: http.error,
    getList,
    add,
    edit,
    delete_,
  };
};
