import { act, screen, render, fireEvent } from "@testing-library/react";
import React from "react";
import { ProjectsForm, ProjectsFormProps, ProjectsTable, ProjectsTableProps } from ".";
import { ProjectFromJSON, StatusFromJSON } from "../../models";
import * as ReactRouter from "react-router";

describe('ProjectsTable', () => {

  jest.spyOn(ReactRouter, 'useNavigate').mockReturnValue(jest.fn(() => jest.fn));
  
  test("ProjectTable when rendered", () => {

      //Arrange
      const mockSetSelected = jest.fn();

      const props: ProjectsTableProps = {
        projects: [ProjectFromJSON({projectId : 1, projectName: "Test 1", description: "Description 1", statusCode: "T"}),
                  ProjectFromJSON({projectId : 2, projectName: "Test 2", description: "Description 2", statusCode: "T"})],
        setSelectedProject: mockSetSelected,
        statuses: [StatusFromJSON({statusCode: "T", statusDesc: "To Do"}), StatusFromJSON({statusCode: "D", statusDesc: "Done"})]
      };
    
      //Act
      render(<ProjectsTable {...props} />);

      //Assert
      const rows = screen.getAllByRole("row");
      expect(rows).toHaveLength(3); // 2 projects + 1 header.
    });

    test("ProjectTable when rendered then empty", () => {

      //Arrange
      const mockSetSelected = jest.fn();
   
      const props: ProjectsTableProps = {
        projects: [],
        setSelectedProject: mockSetSelected,
        statuses: [StatusFromJSON({statusCode: "T", statusDesc: "To Do"}), StatusFromJSON({statusCode: "D", statusDesc: "Done"})]
      };
    
      //Act
      render(<ProjectsTable {...props} />);

      //Assert
      screen.getByText("Projects not found.")
    });

    test("ProjectTable when click, then call setSelected", async () => {

      //Arrange
      const mockSetSelected = jest.fn();
   
      const props: ProjectsTableProps = {
        projects: [ProjectFromJSON({projectId : 1, projectName: "Test 1", description: "Description 1", statusCode: "T"}),
                    ProjectFromJSON({projectId : 2, projectName: "Test 2", description: "Description 2", statusCode: "T"})],
        setSelectedProject: mockSetSelected,
        statuses: [StatusFromJSON({statusCode: "T", statusDesc: "To Do"}), StatusFromJSON({statusCode: "D", statusDesc: "Done"})]
      };
    
      //Act
      render(<ProjectsTable {...props} />);
      const rows = screen.getAllByRole("row");
      expect(rows).toHaveLength(3); // 2 projects + 1 header.

      await act(async () => { fireEvent.click(rows[1]); });

      //Assert
      expect(mockSetSelected).toHaveBeenCalledTimes(1);



    });


});