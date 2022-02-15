import * as React from "react";

type propsType = {
  /*  This indicates if the elements has been touched. From formik */
  touched?: boolean;
  /* This indicates if there is an valdation error in the form. From formik */
  FormError?: string;
  /* This indicates if there are validation errors coming from the API */
  error?: string;
};

export const InputError = ({ touched, error }: propsType) => {
  if (touched && error) {
    return <div className="invalid-feedback">{error}</div>;
  }
  return null;
};
