import { act, screen, render, fireEvent } from "@testing-library/react";
import React from "react";
import { ProjectsForm, ProjectsFormProps } from ".";
import { ProjectFromJSON, StatusFromJSON } from "../../models";
import * as ReactRouter from "react-router";

describe("ProjectsForm", () => {
  jest.spyOn(ReactRouter, "useNavigate").mockReturnValue(jest.fn(() => jest.fn));

  test("ProjectsForm when rendered", () => {
    //Arrange
    const mockHandleSave = jest.fn();

    const props: ProjectsFormProps = {
      project: ProjectFromJSON({ projectId: 1, projectName: "Test 1", description: "Description 1", statusCode: "T" }),
      handleSave: mockHandleSave,
      statuses: [StatusFromJSON({ statusCode: "T", statusDesc: "To Do" }), StatusFromJSON({ statusCode: "D", statusDesc: "Done" })],
    };

    //Act
    render(<ProjectsForm {...props} />);
  });

  test("ProjectsForm when click save and required field is empty, then save is not called", async () => {
    //Arrange
    const mockHandleSave = jest.fn();

    const props: ProjectsFormProps = {
      project: ProjectFromJSON({ projectId: 1, projectName: "", description: "Description 1", statusCode: "T" }),
      handleSave: mockHandleSave,
      statuses: [StatusFromJSON({ statusCode: "T", statusDesc: "To Do" }), StatusFromJSON({ statusCode: "D", statusDesc: "Done" })],
    };

    //Act
    render(<ProjectsForm {...props} />);

    const saveButton = screen.getByRole("button", { name: "Save" });

    await act(async () => {
      fireEvent.click(saveButton);
    });

    //Assert
    expect(mockHandleSave).toHaveBeenCalledTimes(0);
    screen.getByText("Project Name is required");
  });

  test("ProjectsForm when click save, calls handle save", async () => {
    //Arrange
    const mockHandleSave = jest.fn();
    const props: ProjectsFormProps = {
      project: ProjectFromJSON({ projectId: 1, projectName: "Project Name", description: "Description 1", statusCode: "T" }),
      handleSave: mockHandleSave,
      statuses: [StatusFromJSON({ statusCode: "T", statusDesc: "To Do" }), StatusFromJSON({ statusCode: "D", statusDesc: "Done" })],
    };

    //Act
    render(<ProjectsForm {...props} />);

    const addButton = screen.getByRole("button", { name: "Save" });

    await act(async () => {
      fireEvent.click(addButton);
    });

    //Assert
    expect(mockHandleSave).toHaveBeenCalledTimes(1);
  });
});
