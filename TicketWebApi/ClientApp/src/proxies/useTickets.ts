import { TicketFromJSON, Ticket } from "../models";
import { useHttp } from ".";
import { Observable } from "rxjs";

const path = "Tickets";

export type ITicketsProxy = {
  working: boolean;
  error: any| null;
  getList: () => Observable<Ticket[]>;
  search: (projectId:number) => Observable<Ticket[]>;
  add: (ticket: Ticket) => Observable<Ticket>;
  edit: (ticket: Ticket) => Observable<Ticket>;
  delete_: (ticket: Ticket) => Observable<Ticket>;
};

export const useTicketsProxy = (): ITicketsProxy => {
  const http = useHttp();

  const getList= (): Observable<Ticket[]> => {
    return http.getList(path, null, TicketFromJSON);
  };

  const search= (projectId: number): Observable<Ticket[]> => {
    return http.getList(path + "/search/" + projectId, null,  TicketFromJSON);
  };

  const add = (ticket: Ticket) => {
    return http.post<Ticket>(path, ticket, TicketFromJSON);
  };

  const edit = (ticket: Ticket) => {
    return http.put<Ticket>(path, ticket, TicketFromJSON);
  };

  const delete_ = (ticket: Ticket) => {
    return http.delete_<Ticket>(path, ticket, TicketFromJSON);
  };

  return {
    working: http.working,
    error: http.error,
    getList,
    search,
    add,
    edit,
    delete_,
  };
};
