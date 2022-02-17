import React from "react";
import { Link, useParams } from "react-router-dom";
import { useNavigate } from "react-router";

import "./NavMenu.scss";

type propsType = {
  expanded: boolean;
};

export const NavMenu = (props: propsType) => {
  const { expanded } = props;

  const { perm } = useParams<{ perm: string }>();
  const _history = useNavigate();

  if (expanded) return <></>;

  return (
    <nav className="sidebar">
      <div className="scrollbar-container">
        <ul className="sidebar-nav">
          <li className="sidebar-header">
            <Link to="/">Home</Link>
          </li>
          <li className="sidebar-header">
            <Link to="/Projects">Projects</Link>
          </li>
          <li className="sidebar-header">
            <Link to="/Tickets">Tickets</Link>
          </li>
        </ul>
      </div>
    </nav>
  );
};

export default NavMenu;
