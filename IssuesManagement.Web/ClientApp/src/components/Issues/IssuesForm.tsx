import React from "react";
import { Issue, IssueFromJSON, User } from "../../models";
import { Formik } from "formik";
import * as Yup from "yup";
import { FormGroup, Form, Label, Input, Row, Col, Button } from "reactstrap";
import { InputError } from "..";

export type IssuesFormProps = {
  issue: Issue;
  users?: User[] | null;
  handleSave?: any;
};

export const IssuesForm = ({ issue, users, handleSave }: IssuesFormProps) => {
  const isNew = issue.issueId === 0;

  //Form logic
  return (
    <>
      <div>
        <div className="content">
          <Formik
            validationSchema={IssueSchema}
            enableReinitialize={true}
            initialValues={{
              issueId: issue.issueId ?? 0,
              title: issue.title ?? "",
              statusOpened: issue.statusOpened ?? true,
              description: issue.description ?? "",
              assignedTo: issue.assignedTo ?? 0,
            }}
            onSubmit={(values, { setSubmitting }) => {
              handleSave(IssueFromJSON(values));
            }}
          >
            {({ values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting, setFieldValue }) => (
              <Form onSubmit={handleSubmit}>
                {isNew ? <strong>Add Issue</strong> : <strong>Edit Issue</strong>}
                <Row>
                  <Col xs={12}>
                    <FormGroup>
                      <Label>Issue Name</Label>
                      <Input type="text" name="title" value={values.title} onChange={handleChange} onBlur={handleBlur} className={errors.title ? " is-invalid" : ""} />
                      <InputError touched={touched.title} error={errors.title} />
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col xs={12}>
                    <FormGroup>
                      <Label>Issue Description</Label>
                      <Input type="textarea" name="description" value={values.description} onChange={handleChange} onBlur={handleBlur} className={errors.description ? " is-invalid" : ""} />
                      <InputError touched={touched.description} error={errors.description} />
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col xs={12}>
                    <FormGroup>
                      <Label>Status</Label>
                      <Input
                        type="select"
                        name="statusOpened"
                        value={values.statusOpened ? 1 : 0}
                        onChange={(e) => {
                          setFieldValue("statusOpened", e.target.value === "0" ? false : true);
                        }}
                        onBlur={handleBlur}
                        className={errors.statusOpened ? " is-invalid" : ""}
                      >
                        <option key={1} value={1}>
                          Opened
                        </option>
                        <option key={0} value={0}>
                          Closed
                        </option>
                      </Input>
                      <InputError touched={touched.statusOpened} error={errors.statusOpened} />
                    </FormGroup>
                    <Row>
                      <Col xs={12}>
                        <FormGroup>
                          <Label>Assigned To</Label>
                          <Input
                            type="select"
                            name="assignedTo"
                            value={values.assignedTo ?? undefined}
                            onChange={(e) => {
                              const userId = parseInt(e.target.value, 10);
                              setFieldValue("assignedTo", userId);
                            }}
                            onBlur={handleBlur}
                            className={errors.assignedTo ? " is-invalid" : ""}
                          >
                            <option key={0} value={0}>
                              Unassigned
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
                          <InputError touched={touched.assignedTo} error={errors.assignedTo} />
                        </FormGroup>
                      </Col>
                    </Row>
                  </Col>
                </Row>
                <Row>
                  <Col>
                    <Button color="primary" style={{ marginTop: "5px" }} type="submit" disabled={isSubmitting}>
                      Save
                    </Button>{" "}
                  </Col>
                </Row>
              </Form>
            )}
          </Formik>
        </div>
      </div>
    </>
  );
};

//Define Validation Scheme
const IssueSchema = Yup.object().shape({
  title: Yup.string().required("Title is required").max(50, "Max number of characters is 500"),
  description: Yup.string().max(2000, "Max number of characters is 2000"),
  statusOpened: Yup.boolean().required("Status is required"),
});

export default IssuesForm;
