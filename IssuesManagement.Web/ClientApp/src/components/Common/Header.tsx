import React from "react";
import { Link } from "react-router-dom";
import "./Header.scss";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars, faSignOutAlt } from "@fortawesome/free-solid-svg-icons";

type propsType = {
  handleExpand: Function;
  expanded: boolean;
};

export const Header = (props: propsType) => {
  const { handleExpand } = props;

  return (
    <header className="header">
      <nav className="nav-primary navbar navbar-expand ms-2">
        <div>
          <a href="#">
            <FontAwesomeIcon icon={faBars} size="lg" onClick={() => handleExpand()} />
          </a>
        </div>
        <div className="logo">
          <Link to="/">Issues Management</Link>
        </div>
        <div className="navbar-collapse">
          <ul className="ms-auto navbar-nav navbar-expand">
            <li className="me-4 nav-item">
              <a
                href="#"
                onClick={() => {
                  //logout();
                }}
              >
                <FontAwesomeIcon icon={faSignOutAlt} style={{ marginRight: "2px" }} />
                Logout
              </a>
            </li>
          </ul>
        </div>
      </nav>
    </header>
  );
};
