import { IssueFromJSON, Issue } from "../models";
import { useHttp } from ".";
import { Observable } from "rxjs";

const path = "Issues";

export type IIssuesProxy = {
  working: boolean;
  error: any | null;
  getList: () => Observable<Issue[]>;
  search: (userId: number) => Observable<Issue[]>;
  add: (issue: Issue) => Observable<Issue>;
  edit: (issue: Issue) => Observable<Issue>;
};

export const useIssuesProxy = (): IIssuesProxy => {
  const http = useHttp();

  const getList = (): Observable<Issue[]> => {
    return http.getList(path, null, IssueFromJSON);
  };

  const search = (userId: number): Observable<Issue[]> => {
    return http.getList(path + "/search/" + userId, null, IssueFromJSON);
  };

  const add = (issue: Issue) => {
    return http.post<Issue>(path, issue, IssueFromJSON);
  };

  const edit = (issue: Issue) => {
    return http.put<Issue>(path, issue, IssueFromJSON);
  };

  return {
    working: http.working,
    error: http.error,
    getList,
    search,
    add,
    edit,
  };
};
