import React, { useEffect, useState } from "react";
import { Project, ProjectFromJSON, Status } from "../../models";
import { Formik } from "formik";
import * as Yup from "yup";
import { FormGroup, Form, Label, Input, Row, Col, Button } from "reactstrap";
import { InputError } from "..";
import { useNavigate } from "react-router";


export type ProjectsFormProps = {
  project: Project;
  handleSave?: any;
  statuses?: Status[] | null;
};

export const ProjectsForm = ({project, handleSave, statuses}: ProjectsFormProps) => {

  const isNew = project.projectId === 0;
  const navigate = useNavigate();

  const handleSeeTicketsClick = ()=> {
      navigate("/Tickets/" + project.projectId)
  }

  //Form logic
  return (
    <>
      <div>
        <div className="content">
          <Formik
            validationSchema={ProjectSchema}
            enableReinitialize={true}
            initialValues={{
              projectId: project.projectId ?? 0,
              projectName: project.projectName ?? "",
              statusCode: project.statusCode ?? "T",
              description: project.description ?? "",
            }}
            onSubmit={(values, { setSubmitting }) => {
                handleSave(ProjectFromJSON(values));
            }}
          >
            {({ values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting}) => (
              <Form onSubmit={handleSubmit}>
                {isNew ? <strong>Add Project</strong> : <strong>Edit Project</strong>}
                <Row>
                    <Col xs={12}>
                        <FormGroup>
                            <Label>Project Name</Label>
                            <Input
                            type="text"
                            name="projectName"
                            value={values.projectName}
                            onChange={handleChange}
                            onBlur={handleBlur}
                            className={errors.projectName ? " is-invalid" : ""}
                            />
                            <InputError touched={touched.projectName} error={errors.projectName} />
                        </FormGroup>
                    </Col>
                </Row>
                <Row>
                    <Col xs={12}>
                        <FormGroup>
                            <Label>Project Description</Label>
                            <Input
                            type="textarea"
                            name="description"
                            value={values.description}
                            onChange={handleChange}
                            onBlur={handleBlur}
                            className={errors.description ? " is-invalid" : ""}
                            />
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
                            name="statusCode"
                            value={values.statusCode}
                            onChange={handleChange}
                            onBlur={handleBlur}
                            className={errors.statusCode ? " is-invalid" : ""}
                            >
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
                    <Col> 
                        <Button color="primary" style={{marginTop:'5px'}} type="submit" disabled={isSubmitting}>
                        Save
                        </Button>{" "}
                        {!isNew && 
                            <Button color="primary" style={{marginTop:'5px'}} disabled={isSubmitting} onClick={handleSeeTicketsClick} >
                            See Tickets
                            </Button>
                        }
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
const ProjectSchema = Yup.object().shape({
   projectName: Yup.string().required("Project Name is required").max(500, "Max number of characters is 500"),
   description: Yup.string().max(2000, "Max number of characters is 2000"),
   statusCode: Yup.string().required("Status is required"),
});

export default ProjectsForm;

