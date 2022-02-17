import { act, screen, render, fireEvent } from "@testing-library/react";
import React from "react";
import { ProjectsForm, ProjectsFormProps, ProjectsPage, ProjectsTable, ProjectsTableProps } from ".";
import { Project, ProjectFromJSON, Status, StatusFromJSON } from "../../models";
import { IProjectsProxy, IStatusProxy } from "../../proxies";
import { of } from "rxjs";
import * as ReactRouter from "react-router";

describe('ProjectsTable', () => {

  jest.spyOn(ReactRouter, 'useNavigate').mockReturnValue(jest.fn(() => jest.fn));

  const fakeProjects: Project[] = [ProjectFromJSON({projectId : 1, projectName: "Test 1", description: "Description 1", statusCode: "T"}),
                                    ProjectFromJSON({projectId : 2, projectName: "Test 2", description: "Description 2", statusCode: "T"})]

  const fakeStatus: Status[] =  [StatusFromJSON({statusCode: "T", statusDesc: "To Do"}), StatusFromJSON({statusCode: "D", statusDesc: "Done"})]

  const mockProjectsProxy: IProjectsProxy = {
    working: false,
    error: null,
    add:  jest.fn(),
    edit: jest.fn(),
    delete_: jest.fn(),
    getList: () => of(fakeProjects),
  };

  const mockStatusProxy: IStatusProxy = {
    working: false,
    error: null,
    getList: () => of(fakeStatus),
  };
  
  test("ProjectsPage when rendered", () => {

      //Arrange
      const mockSetSelected = jest.fn();
    
      //Act
      render(<ProjectsPage />);
    });

    test("ProjectsPage when click Add, display form", async () => {

      //Arrange
      const mockSetSelected = jest.fn();
    
      //Act
      render(<ProjectsPage />);

      const addButton = await screen.findByRole("button", { name: "Add" });

      await act(async () => {    fireEvent.click(addButton); });

      //Assert
      //Search for form elements
      screen.getByText("Add Project")
      screen.getByText("Project Name")
      screen.getByRole("button", { name: "Save" });
    });
});

