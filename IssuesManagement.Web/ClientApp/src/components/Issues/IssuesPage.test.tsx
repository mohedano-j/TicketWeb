import { act, screen, render, fireEvent } from "@testing-library/react";
import React from "react";
import { IssuesPage } from ".";
import { Issue, IssueFromJSON, User, UserFromJSON } from "../../models";
import { IIssuesProxy, IStatusProxy, IUsersProxy } from "../../proxies";
import { of } from "rxjs";

describe("IssuesTable", () => {
  const fakeIssues: Issue[] = [
    IssueFromJSON({ issueId: 1, issueName: "Test 1", description: "Description 1", statusOpened: true, status: "Opened" }),
    IssueFromJSON({ issueId: 2, issueName: "Test 2", description: "Description 2", statusOpened: true, status: "Opened" }),
  ];

  const fakeUsers: User[] = [UserFromJSON({ userId: 1, useName: "AAAA", firstName: "John", lastName: "Doe" }), UserFromJSON({ userId: 2, useName: "BBB", firstName: "Mary", lastName: "Doe" })];

  const mockIssuesProxy: IIssuesProxy = {
    working: false,
    error: null,
    add: jest.fn(),
    edit: jest.fn(),
    getList: () => of(fakeIssues),
    search: () => of(fakeIssues),
  };

  const mockUsersProxy: IUsersProxy = {
    working: false,
    error: null,
    getList: () => of(fakeUsers),
  };

  test("IssuesPage when rendered", () => {
    //Arrange
    const mockSetSelected = jest.fn();

    //Act
    render(<IssuesPage />);
  });

  test("IssuesPage when click Add, display form", async () => {
    //Arrange
    const mockSetSelected = jest.fn();

    //Act
    render(<IssuesPage />);

    const addButton = await screen.findByRole("button", { name: "Add" });

    await act(async () => {
      fireEvent.click(addButton);
    });

    //Assert
    //Search for form elements
    screen.getByText("Add Issue");
    screen.getByText("Issue Name");
    screen.getByRole("button", { name: "Save" });
  });
});
