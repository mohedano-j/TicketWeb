import * as React from "react";
import { NavMenu } from "./NavMenu";
import { Header } from "./Header";
import { useToggle } from "react-use";
import { isMobileLayout } from "../../utils";

export const Layout = (props: any) => {
	const isMobile: boolean = isMobileLayout();
	const [expanded, expandedToggle] = useToggle(isMobile);

	const handleExpand = () => {
		expandedToggle();
	};

	return (
		<div className={expanded ? "wrapper expanded" : "wrapper"}>
			<main>
				<Header handleExpand={handleExpand} expanded={expanded} />
				<NavMenu expanded={expanded} />
				<div className="content margin-header">{props.children}</div>
			</main>
		</div>
	);
};

export default Layout;
