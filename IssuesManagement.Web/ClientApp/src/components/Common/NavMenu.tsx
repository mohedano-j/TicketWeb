import React from "react";
import { Link, useParams } from "react-router-dom";
import { useNavigate } from "react-router";

import "./NavMenu.scss";

type propsType = {
  expanded: boolean;
};

export const NavMenu = (props: propsType) => {
  const { expanded } = props;

  if (expanded) return <></>;

  return (
    <nav className="sidebar">
      <div className="scrollbar-container">
        <ul className="sidebar-nav">
          <li className="sidebar-header">
            <Link to="/">Home</Link>
          </li>
          <li className="sidebar-header">
            <Link to="/Issues">Issues</Link>
          </li>
        </ul>
      </div>
    </nav>
  );
};

export default NavMenu;
