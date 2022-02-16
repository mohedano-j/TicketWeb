import React from "react";
import { ToastContainer } from "react-toastify";

import "react-toastify/dist/ReactToastify.css";
import "./fontawesome";

import { Layout, Spinner } from "./components";
import AppRoutes from "./AppRoutes";

export const App = () => {
  return (
    <>
      <ToastContainer />
      <Layout>
        <AppRoutes />
      </Layout>
    </>
  );
};
