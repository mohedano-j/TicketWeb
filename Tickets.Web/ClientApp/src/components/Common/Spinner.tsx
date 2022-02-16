import * as React from "react";
import "./Spinner.scss";

export const Spinner = () => {
  return (
    <div role="alert" aria-live="assertive" className="loader">
      Loading...
    </div>
  );
};

export default Spinner;
