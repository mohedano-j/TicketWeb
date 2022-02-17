import { act, screen, render, fireEvent } from "@testing-library/react";
import React from "react";
import { IssuesTable, IssuesTableProps } from ".";
import { IssueFromJSON } from "../../models";

describe("IssuesTable", () => {
  test("IssueTable when rendered", () => {
    //Arrange
    const mockSetSelected = jest.fn();

    const props: IssuesTableProps = {
      issues: [
        IssueFromJSON({ issueId: 1, issueName: "Test 1", description: "Description 1", statusOpened: true, status: "Opened" }),
        IssueFromJSON({ issueId: 2, issueName: "Test 2", description: "Description 2", statusOpened: true, status: "Opened" }),
      ],
      setSelectedIssue: mockSetSelected,
    };

    //Act
    render(<IssuesTable {...props} />);

    //Assert
    const rows = screen.getAllByRole("row");
    expect(rows).toHaveLength(3); // 2 issues + 1 header.
  });

  test("IssueTable when rendered then empty", () => {
    //Arrange
    const mockSetSelected = jest.fn();

    const props: IssuesTableProps = {
      issues: [],
      setSelectedIssue: mockSetSelected,
    };

    //Act
    render(<IssuesTable {...props} />);

    //Assert
    screen.getByText("Issues not found.");
  });

  test("IssueTable when click, then call setSelected", async () => {
    //Arrange
    const mockSetSelected = jest.fn();

    const props: IssuesTableProps = {
      issues: [
        IssueFromJSON({ issueId: 1, issueName: "Test 1", description: "Description 1", statusOpened: true, status: "Opened" }),
        IssueFromJSON({ issueId: 2, issueName: "Test 2", description: "Description 2", statusOpened: true, status: "Opened" }),
      ],
      setSelectedIssue: mockSetSelected,
    };

    //Act
    render(<IssuesTable {...props} />);
    const rows = screen.getAllByRole("row");
    expect(rows).toHaveLength(3); // 2 issues + 1 header.

    await act(async () => {
      fireEvent.click(rows[1]);
    });

    //Assert
    expect(mockSetSelected).toHaveBeenCalledTimes(1);
  });
});
