import * as React from "react";
import { Input } from "reactstrap";
import { InputError } from ".";

type propsType = {
  touched?: boolean;
  error?: string;
  value?: string;
  name: string;
  placeholder?: string;
  labelBy?: string;
  maxLength: number;
  handleChange: any;
  handleBlur: any;
};

export const LimitedTextArea = (props: propsType) => {
  const { touched, error, value, name, placeholder, labelBy, maxLength, handleChange, handleBlur } = props;
  return (
    <>
      <Input
        type="textarea"
        name={name}
        maxLength={maxLength}
        value={value}
        placeholder={placeholder}
        rows={11}
        onChange={handleChange}
        onBlur={handleBlur}
        aria-labelledby={labelBy}
        className={error && touched ? " is-invalid" : ""}
      />
      <small style={{ textAlign: "right" }} id={name + "Help"} className="form-text text-muted">
        {value?.length?.toString() ?? "0"} / <strong>{maxLength.toString()}</strong>
      </small>
      <InputError touched={touched} error={error} />
    </>
  );
};

export default LimitedTextArea;
