import React from "react";
import { Ticket, TicketFromJSON, Status, Employee } from "../../models";
import { Formik } from "formik";
import * as Yup from "yup";
import { FormGroup, Form, Label, Input, Row, Col, Button } from "reactstrap";
import { InputError } from "..";

export type TicketsFormProps = {
  ticket: Ticket;
  handleSave?: any;
  statuses?: Status[] | null;
  employees?: Employee[] | null;
};

export const TicketsForm = ({ ticket, handleSave, statuses, employees }: TicketsFormProps) => {
  const isNew = ticket.ticketId === 0;

  //Form logic
  return (
    <>
      <div>
        <div className="content">
          <Formik
            validationSchema={TicketSchema}
            enableReinitialize={true}
            initialValues={{
              projectId: ticket.projectId,
              ticketId: ticket.ticketId ?? 0,
              title: ticket.title ?? "",
              statusCode: ticket.statusCode ?? "T",
              employeeId: ticket.employeeId ?? undefined,
              description: ticket.description ?? "",
            }}
            onSubmit={(values, { setSubmitting }) => {
              handleSave(TicketFromJSON(values));
            }}
          >
            {({ values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting }) => (
              <Form onSubmit={handleSubmit}>
                {isNew ? <strong>Add Ticket</strong> : <strong>Edit Ticket</strong>}
                <Row>
                  <Col xs={12}>
                    <FormGroup>
                      <Label>Ticket Name</Label>
                      <Input type="text" name="title" value={values.title} onChange={handleChange} onBlur={handleBlur} className={errors.title ? " is-invalid" : ""} />
                      <InputError touched={touched.title} error={errors.title} />
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col xs={12}>
                    <FormGroup>
                      <Label>Ticket Description</Label>
                      <Input type="textarea" name="description" value={values.description} onChange={handleChange} onBlur={handleBlur} className={errors.description ? " is-invalid" : ""} />
                      <InputError touched={touched.description} error={errors.description} />
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col xs={12}>
                    <FormGroup>
                      <Label>Status</Label>
                      <Input type="select" name="statusCode" value={values.statusCode} onChange={handleChange} onBlur={handleBlur} className={errors.statusCode ? " is-invalid" : ""}>
                        {statuses &&
                          statuses.map((status: Status) =>
                            status.statusCode ? (
                              <option key={status.statusCode} value={status.statusCode}>
                                {status.statusDesc}
                              </option>
                            ) : null
                          )}
                      </Input>
                      <InputError touched={touched.statusCode} error={errors.statusCode} />
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col xs={12}>
                    <FormGroup>
                      <Label>Assigned To</Label>
                      <Input type="select" name="employeeId" value={values.employeeId ?? undefined} onChange={handleChange} onBlur={handleBlur} className={errors.employeeId ? " is-invalid" : ""}>
                        <option key={0} value={0}>
                          Unassigned
                        </option>
                        {employees &&
                          employees.map((employee: Employee) =>
                            employee.employeeId ? (
                              <option key={employee.employeeId} value={employee.employeeId}>
                                {employee.firstName + " " + employee.lastName}
                              </option>
                            ) : null
                          )}
                      </Input>
                      <InputError touched={touched.statusCode} error={errors.statusCode} />
                    </FormGroup>
                  </Col>
                </Row>
                <Row>
                  <Col>
                    <Button color="primary" style={{ marginTop: "5px" }} type="submit">
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
const TicketSchema = Yup.object().shape({
  title: Yup.string().required("Title is required").max(500, "Max number of characters is 500"),
  description: Yup.string().max(2000, "Max number of characters is 2000"),
  statusCode: Yup.string().required("Status is required"),
});

export default TicketsForm;
