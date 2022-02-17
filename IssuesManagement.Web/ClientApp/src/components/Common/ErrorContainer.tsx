import * as React from "react";

type propsType = {
	error?: any | null;
};

export const ErrorContainer = ({ error }: propsType) => {
	if (error) {
		return (
			<div className="alert alert-danger" role="alert">
				<div className="alert-heading font-weight-bold">An error occured in the application.</div>
			</div>
		);
	}
	return null;
};

export default ErrorContainer;
