import { act, screen, render, fireEvent } from "@testing-library/react";
import React from "react";
import { IssuesForm, IssuesFormProps } from ".";
import { IssueFromJSON, StatusFromJSON } from "../../models";

describe("IssuesForm", () => {
  test("IssuesForm when rendered", () => {
    //Arrange
    const mockHandleSave = jest.fn();

    const props: IssuesFormProps = {
      issue: IssueFromJSON({ issueId: 1, title: "Test 1", description: "Description 1", statusOpened: true }),
      handleSave: mockHandleSave,
    };

    //Act
    render(<IssuesForm {...props} />);
  });

  test("IssuesForm when click save and required field is empty, then save is not called", async () => {
    //Arrange
    const mockHandleSave = jest.fn();

    const props: IssuesFormProps = {
      issue: IssueFromJSON({ issueId: 1, title: "", description: "Description 1", statusOpened: true }),
      handleSave: mockHandleSave,
    };

    //Act
    render(<IssuesForm {...props} />);

    const saveButton = screen.getByRole("button", { name: "Save" });

    await act(async () => {
      fireEvent.click(saveButton);
    });

    //Assert
    expect(mockHandleSave).toHaveBeenCalledTimes(0);
    screen.getByText("Title is required");
  });

  test("IssuesForm when click save, calls handle save", async () => {
    //Arrange
    const mockHandleSave = jest.fn();
    const props: IssuesFormProps = {
      issue: IssueFromJSON({ issueId: 1, title: "Issue Name", description: "Description 1", statusOpened: true }),
      handleSave: mockHandleSave,
    };

    //Act
    render(<IssuesForm {...props} />);

    const addButton = screen.getByRole("button", { name: "Save" });

    await act(async () => {
      fireEvent.click(addButton);
    });

    //Assert
    expect(mockHandleSave).toHaveBeenCalledTimes(1);
  });
});
