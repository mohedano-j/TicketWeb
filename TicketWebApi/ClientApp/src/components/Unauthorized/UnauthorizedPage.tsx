import React from "react";
import { Alert } from "reactstrap";

export const UnauthorizedPage = () => (
	<div>
		<Alert color="danger">You are not authorized to view the page you were attempting to visit.</Alert>
	</div>
);

export default UnauthorizedPage;
