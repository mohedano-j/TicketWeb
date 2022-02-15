import React from "react";
import { Collapse } from "reactstrap";
import { Link, useParams } from "react-router-dom";
import { useNavigate } from "react-router";

import "./NavMenu.scss";
import { useToggle } from "react-use";
import { pretty } from "../../utils";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser } from "@fortawesome/free-solid-svg-icons";

type propsType = {
	expanded: boolean;
};

export const NavMenu = (props: propsType) => {
	const { expanded } = props;
	const [showStudent, toggleShowStudent] = useToggle(true);
	const [showAdmin, toggleShowAdmin] = useToggle(false);
	const [showRecentSearch, toggleShowRecentSearch] = useToggle(true);

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
						<Link to="/Grades">Grades</Link>
					</li>
					<li className="sidebar-header">
						<Link to="/Transcripts">Unnofficial Transcripts</Link>
					</li>
					<li className="sidebar-header">
						<Link to="/Transcripts">Unnofficial Verifications</Link>
					</li>
					<li className="sidebar-header">
						<Link to="/Transcripts">Official Transcripts & Verifications</Link>
					</li>
				</ul>
			</div>
		</nav>
	);
};

export default NavMenu;
