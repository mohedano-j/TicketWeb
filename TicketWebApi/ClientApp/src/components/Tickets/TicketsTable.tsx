import React, { Dispatch, SetStateAction } from "react";
import { DataGrid } from '@mui/x-data-grid';
import { Ticket, Status, Employee } from "../../models";

export type TicketsTableProps= {
    /*  This indicates if the elements has been touched. From formik */
    tickets?: Ticket[] | null;
    selectedTicket?: Ticket | null;
    setSelectedTicket: Dispatch<SetStateAction<Ticket | null | undefined>>;
  };

export const TicketsTable = (props: TicketsTableProps) => {
    const {tickets, selectedTicket, setSelectedTicket} = props;

    const columns = [
        { field: 'ticketId', headerName: 'ID', width: 50 },
        { field: 'title', headerName: 'Title', width: 180 },
        { field: 'description', headerName: 'Description', width: 250 },
        { field: 'status', headerName: 'Status', width: 150,  valueGetter: (params: { row: { status: Status } } ) =>  params.row.status.statusDesc },
        { field: 'employee', headerName: 'Assigned To', width: 160,  
          valueGetter: (params: { row: { employee: Employee } } ) => params.row.employee ? params.row.employee.firstName + " " + params.row.employee.lastName : "Unassigned"}
      ];
      
      if (tickets === undefined || tickets == null || tickets?.length === 0  ) return  <div>Tickets not found.</div>;

      return (
        <div className="data-grid-responsive" style={{marginTop: "5px"}}>
          <DataGrid
            rows={tickets}
            columns={columns}
            getRowId={(row) => row.ticketId}
            pageSize={10}
            rowsPerPageOptions={[10]}
            selectionModel={selectedTicket?.ticketId}
            onSelectionModelChange={(rowsSelected) => {
              const id = rowsSelected[0];
              const ticket = tickets.find(p=>p.ticketId === id )
              setSelectedTicket(ticket);
            }}
          />
        </div>);

}