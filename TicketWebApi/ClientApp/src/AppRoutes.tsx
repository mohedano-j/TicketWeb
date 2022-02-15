import * as React from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import { HomePage, NotFoundPage, UnauthorizedPage } from "./components";

export const AppRoutes = () => {
	return (
		<Routes>
			<Route path="/" element={<HomePage />} />
			<Route path="/Search" element={<HomePage />} />
			<Route path="/Unauthorized" element={<UnauthorizedPage />} />
			<Route path="/NotFound" element={<NotFoundPage />} />
			<Route path="*" element={<Navigate to="/NotFound" replace />} />
		</Routes>
	);
};

export default AppRoutes;
