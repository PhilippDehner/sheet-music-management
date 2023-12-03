import { Outlet } from "react-router-dom";
import "../App.css";
// import {  Stack } from "react-bootstrap";
import NavigationBar from "./NavBar";




const Layout = () => {

	return (
		<>
			<NavigationBar />
			<Outlet />
			{/* <div className="footer">
				<Stack direction="horizontal" >
				
				</Stack>
			</div> */}
		</>
	);
};

export default Layout;