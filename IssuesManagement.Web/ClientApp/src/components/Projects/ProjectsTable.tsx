import React, { Dispatch, SetStateAction } from "react";
import { DataGrid } from "@mui/x-data-grid";
import { Project, Status } from "../../models";

export type ProjectsTableProps = {
  projects?: Project[] | null;
  selectedProject?: Project | null;
  setSelectedProject: Dispatch<SetStateAction<Project | null | undefined>>;
};

export const ProjectsTable = (props: ProjectsTableProps) => {
  const { projects, selectedProject, setSelectedProject } = props;

  const columns = [
    { field: "projectId", headerName: "ID", width: 70 },
    { field: "projectName", headerName: "Project Name", width: 130 },
    { field: "description", headerName: "Description", width: 350 },
    { field: "status", headerName: "Status", width: 150, valueGetter: (params: { row: { status: Status } }) => params.row.status.statusDesc },
  ];

  if (projects === undefined || projects == null || projects?.length === 0) return <div>Projects not found.</div>;

  return (
    <div className="data-grid-responsive">
      <DataGrid
        rows={projects}
        columns={columns}
        getRowId={(row) => row.projectId}
        pageSize={10}
        rowsPerPageOptions={[10]}
        selectionModel={selectedProject?.projectId}
        onSelectionModelChange={(rowsSelected) => {
          const id = rowsSelected[0];
          const project = projects.find((p) => p.projectId === id);
          setSelectedProject(project);
        }}
      />
    </div>
  );
};
