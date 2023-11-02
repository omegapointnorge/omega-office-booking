
import { Navbar, Nav, NavbarBrand, NavItem, NavLink } from 'reactstrap';
import { useLocation } from "react-router-dom";
import { Link } from 'react-router-dom';
import React from 'react';

export const NavigationBar = () => {

    const { location }= useLocation();
 

// var username = activeAccount.name : 'Unknown'
    /**
     *   const linkUrl = "https://localhost:5001/api/Account/Login";
     * Most applications will need to conditionally render certain components based on whether a user is signed in or not.
     */
    const baseUrl = "https://localhost:5001";
    const LoginUrl = baseUrl+"/api/Account/Login";
    const LogoutUrl = baseUrl+"/api/Account/Logout";
    return (
        <>
        <header>
            <Navbar color="light" light expand="md">
            <div>
                   
                 <a className="btn btn-primary" href={LoginUrl}>
                    Login
                </a>
                <a className="btn btn-primary" href={LogoutUrl}>
                    Sign out
                </a>
               
         
              </div>
            <NavbarBrand tag={Link} to="/">Office booking Solution</NavbarBrand>
            <Nav className="ml-auto" navbar>
                    <ul className="navbar-nav flex-grow">
      <NavItem>
        <NavLink tag={Link} to="/">Home</NavLink>
      </NavItem>
      <NavItem>
        <NavLink tag={Link} to="/Booking">booking</NavLink>
      </NavItem>
      <NavItem>
        <NavLink tag={Link} to="/Overview">Overview data</NavLink>
      </NavItem>
      </ul>
      </Nav>
</Navbar>
</header>

  </>
    );
};

export default NavigationBar;