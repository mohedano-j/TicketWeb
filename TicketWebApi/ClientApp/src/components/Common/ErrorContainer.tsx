import * as React from "react";

type propsType = {
	error?: any | null;
};

export const ErrorContainer = ({ error }: propsType) => {
	if (error) {
		return (
			<div className="alert alert-danger" role="alert">
				<div className="alert-heading font-weight-bold">{error.title}</div>
				{error.detail && <span>{error.detail}</span>}
			</div>
		);
	}
	return null;
};

export default ErrorContainer;
