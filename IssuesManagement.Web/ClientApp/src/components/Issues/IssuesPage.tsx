import React, { useEffect, useState } from "react";
import { Button, Col, Input, Label, Row, Spinner } from "reactstrap";
import { ErrorContainer } from "..";
import { Employee, Issue, IssueFromJSON, User } from "../../models";
import { useIssuesProxy, useUsersProxy } from "../../proxies";
import { IssuesForm, IssuesTable } from ".";
import { toast } from "react-toastify";

export const IssuesPage = () => {
  const [issues, setIssues] = useState<Issue[] | null | undefined>(null);
  const [users, setUsers] = useState<User[] | null | undefined>(null);
  const [selectedUserId, setSelectedUserId] = useState<number>(0);
  const [selectedIssue, setSelectedIssue] = useState<Issue | null | undefined>(null);

  const issuesProxy = useIssuesProxy();
  const usersProxy = useUsersProxy();

  // Load the list of available users the first time.
  useEffect(() => {
    if (!issuesProxy.working && !issuesProxy.error && issues == null) {
      loadUsers();
    }
  }, []);

  const loadUsers = () => {
    usersProxy.getList().subscribe(
      (result: User[]) => {
        setUsers(result);
      },
      (error: any) => {
        setUsers(null);
      }
    );
  };

  // Load the list of available issues the first time.
  useEffect(() => {
    if (!issuesProxy.working && !issuesProxy.error) {
      loadIssues();
    }
  }, [selectedUserId]);

  const loadIssues = () => {
    if (selectedUserId === 0) {
      issuesProxy.getList().subscribe(
        (result: Issue[]) => {
          setIssues(result);
        },
        (error: any) => {
          setIssues(null);
        }
      );
    } else {
      issuesProxy.search(selectedUserId).subscribe(
        (result: Issue[]) => {
          setIssues(result);
        },
        (error: any) => {
          setIssues(null);
        }
      );
    }
  };

  const handleAddClick = () => {
    const newIssue = IssueFromJSON({ issueId: 0 });
    setSelectedIssue(newIssue);
  };

  const handleSave = (issue: Issue) => {
    if (issue.issueId === 0) {
      issuesProxy.add(issue).subscribe((resp: Issue) => {
        if (issues) {
          setIssues([...issues, { ...resp }]);
          setSelectedIssue(resp);
          toast.success("Issue " + resp.title + "  added!");
        }
      });
    } else {
      issuesProxy.edit(issue).subscribe((resp: Issue) => {
        if (issues) {
          const issuesAux = issues.map((issue) => (resp && issue.issueId == resp.issueId ? resp : issue));
          setIssues(issuesAux);
          setSelectedIssue(resp);
          toast.success("Issue " + resp.title + "  modified!");
        }
      });
    }
  };

  if (issuesProxy.working) return <Spinner />;

  return (
    <>
      {issuesProxy.error != null && <ErrorContainer error={issuesProxy.error} />}
      <div>
        <Row>
          <Col sm={3} xs={12}>
            <Label>Assigned To</Label>
            <Input
              type="select"
              name="user"
              value={selectedUserId ? selectedUserId : 0}
              onChange={(e) => {
                const userId = parseInt(e.target.value, 10);
                setSelectedUserId(userId);
              }}
            >
              <option key={0} value={0}>
                All
              </option>
              {users &&
                users.map((user: User) =>
                  user.userId ? (
                    <option key={user.userId} value={user.userId}>
                      {user.firstName + " " + user.lastName}
                    </option>
                  ) : null
                )}
            </Input>
          </Col>
        </Row>
      </div>
      <Row>
        <Col sm={6} xs={12}>
          <div className="d-flex flex-row-reverse">
            <Button color="primary" style={{ margin: "5px" }} onClick={handleAddClick}>
              Add
            </Button>{" "}
          </div>
          <IssuesTable issues={issues} selectedIssue={selectedIssue} setSelectedIssue={setSelectedIssue} />
        </Col>
        <Col sm={6} xs={12}>
          {selectedIssue && <IssuesForm issue={selectedIssue} handleSave={handleSave} users={users} />}
        </Col>
      </Row>
    </>
  );
};
