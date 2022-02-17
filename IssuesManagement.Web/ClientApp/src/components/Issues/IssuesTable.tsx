import React, { Dispatch, SetStateAction } from "react";
import { DataGrid } from "@mui/x-data-grid";
import { Issue, Status, User } from "../../models";

export type IssuesTableProps = {
  issues?: Issue[] | null;
  selectedIssue?: Issue | null;
  setSelectedIssue: Dispatch<SetStateAction<Issue | null | undefined>>;
};

export const IssuesTable = (props: IssuesTableProps) => {
  const { issues, selectedIssue, setSelectedIssue } = props;

  const columns = [
    { field: "issueId", headerName: "ID", width: 60 },
    { field: "title", headerName: "Title", width: 150 },
    { field: "description", headerName: "Description", width: 250 },
    { field: "status", headerName: "Status", width: 100 },
    {
      field: "assigneTo",
      headerName: "Assigned To",
      width: 150,
      valueGetter: (params: { row: { assignedToNavigation: User } }) =>
        params.row.assignedToNavigation ? params.row.assignedToNavigation.firstName + " " + params.row.assignedToNavigation.lastName : "Unassigned",
    },
  ];

  if (issues === undefined || issues == null || issues?.length === 0) return <div>Issues not found.</div>;

  return (
    <div className="data-grid-responsive">
      <DataGrid
        rows={issues}
        columns={columns}
        getRowId={(row) => row.issueId}
        pageSize={10}
        rowsPerPageOptions={[10]}
        selectionModel={selectedIssue?.issueId}
        onSelectionModelChange={(rowsSelected) => {
          const id = rowsSelected[0];
          const issue = issues.find((p) => p.issueId === id);
          setSelectedIssue(issue);
        }}
      />
    </div>
  );
};
