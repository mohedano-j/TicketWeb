import React, { useEffect, useState } from "react";
import { Button, Col, Row, Spinner } from "reactstrap";
import { ErrorContainer } from "..";
import { Issue, IssueFromJSON } from "../../models";
import { useIssuesProxy } from "../../proxies";
import { IssuesForm, IssuesTable } from ".";
import { toast } from "react-toastify";

export const IssuesPage = () => {
  const [issues, setIssues] = useState<Issue[] | null | undefined>(null);
  const [selectedIssue, setSelectedIssue] = useState<Issue | null | undefined>(null);

  const issueProxy = useIssuesProxy();

  // Load the list of available issues the first time.
  useEffect(() => {
    if (!issueProxy.working && !issueProxy.error && issues == null) {
      loadIssues();
    }
  }, []);

  const loadIssues = () => {
    issueProxy.getList().subscribe(
      (result: Issue[]) => {
        setIssues(result);
      },
      (error: any) => {
        setIssues(null);
      }
    );
  };

  const handleAddClick = () => {
    const newIssue = IssueFromJSON({ issueId: 0 });
    setSelectedIssue(newIssue);
  };

  const handleSave = (issue: Issue) => {
    if (issue.issueId === 0) {
      issueProxy.add(issue).subscribe((resp: Issue) => {
        if (issues) {
          setIssues([...issues, { ...resp }]);
          setSelectedIssue(resp);
          toast.success("Issue " + resp.title + "  added!");
        }
      });
    } else {
      issueProxy.edit(issue).subscribe((resp: Issue) => {
        if (issues) {
          const issuesAux = issues.map((issue) => (resp && issue.issueId == resp.issueId ? resp : issue));
          setIssues(issuesAux);
          setSelectedIssue(resp);
          toast.success("Issue " + resp.title + "  modified!");
        }
      });
    }
  };

  if (issueProxy.working) return <Spinner />;

  return (
    <>
      {issueProxy.error != null && <ErrorContainer error={issueProxy.error} />}

      <Row>
        <Col sm={6} xs={12}>
          <div className="d-flex flex-row-reverse">
            <Button color="primary" style={{ marginBottom: "5px" }} onClick={handleAddClick}>
              Add
            </Button>{" "}
          </div>
          <IssuesTable issues={issues} selectedIssue={selectedIssue} setSelectedIssue={setSelectedIssue} />
        </Col>
        <Col sm={6} xs={12}>
          {selectedIssue && <IssuesForm issue={selectedIssue} handleSave={handleSave} />}
        </Col>
      </Row>
    </>
  );
};
