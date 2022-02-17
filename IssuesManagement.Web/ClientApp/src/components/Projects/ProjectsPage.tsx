import React, { useEffect, useState } from "react";
import { Button, Col, Row, Spinner } from "reactstrap";
import { ErrorContainer } from "..";
import { Project, ProjectFromJSON, ProjectToJSON, Status } from "../../models";
import { useProjectsProxy, useStatusProxy } from "../../proxies";
import { ProjectsForm, ProjectsTable } from ".";
import { toast } from "react-toastify";

export const ProjectsPage = () => {
  const [projects, setProjects] = useState<Project[] | null | undefined>(null);
  const [statuses, setStatuses] = useState<Status[] | null | undefined>(null);
  const [selectedProject, setSelectedProject] = useState<Project | null | undefined>(null);

  const projectProxy = useProjectsProxy();
  const statusProxy = useStatusProxy();

  // Load the list of available projects the first time.
  useEffect(() => {
    if (!projectProxy.working && !projectProxy.error && projects == null) {
      loadProjects();
    }
  }, []);

  const loadProjects = () => {
    projectProxy.getList().subscribe(
      (result: Project[]) => {
        setProjects(result);
      },
      (error: any) => {
        setProjects(null);
      }
    );
  };

  // Load the list of available status to pass it to forms the first time.
  useEffect(() => {
    if (!statusProxy.working && !statusProxy.error && statuses == null) {
      loadStatuses();
    }
  }, []);

  const loadStatuses = () => {
    statusProxy.getList().subscribe(
      (result: Status[]) => {
        setStatuses(result);
      },
      (error: any) => {
        setStatuses(null);
      }
    );
  };

  const handleAddClick = () => {
    const newProject = ProjectFromJSON({ projectId: 0 });
    setSelectedProject(newProject);
  };

  const handleSave = (project: Project) => {
    if (project.projectId === 0) {
      projectProxy.add(project).subscribe((resp: Project) => {
        if (projects) {
          setProjects([...projects, { ...resp }]);
          setSelectedProject(resp);
          toast.success("Project " + resp.projectName + "  added!");
        }
      });
    } else {
      projectProxy.edit(project).subscribe((resp: Project) => {
        if (projects) {
          const projectsAux = projects.map((project) => (resp && project.projectId == resp.projectId ? resp : project));
          setProjects(projectsAux);
          setSelectedProject(resp);
          toast.success("Project " + resp.projectName + "  modified!");
        }
      });
    }
  };

  if (projectProxy.working) return <Spinner />;
  if (statusProxy.working) return <Spinner />;

  return (
    <>
      {projectProxy.error != null && <ErrorContainer error={projectProxy.error} />}
      {statusProxy.error != null && <ErrorContainer error={statusProxy.error} />}

      <Row>
        <Col sm={6} xs={12}>
          <div className="d-flex flex-row-reverse">
            <Button color="primary" style={{ marginBottom: "5px" }} onClick={handleAddClick}>
              Add
            </Button>{" "}
          </div>
          <ProjectsTable projects={projects} selectedProject={selectedProject} setSelectedProject={setSelectedProject} />
        </Col>
        <Col sm={6} xs={12}>
          {selectedProject && <ProjectsForm project={selectedProject} handleSave={handleSave} statuses={statuses} />}
        </Col>
      </Row>
    </>
  );
};
