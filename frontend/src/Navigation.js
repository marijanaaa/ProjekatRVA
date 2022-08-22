import React, {Component} from 'react';  
import { NavLink } from 'react-router-dom';      
import { Navbar, Nav } from 'react-bootstrap';
import './App.css'

export class Navigation extends Component {
    constructor(props){
        super(props);
    }
    render() {
        return (
            <Navbar bg="secondary" expand="lg" className="gap-3 px-3">
                <Navbar.Toggle aria-controls="basic-navbar-nav"/>
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="flex-grow-1 justify-content-evenly">
                        <NavLink className={({isActive}) => 'list-group-item'+(isActive ?' demo' : 'inactive')} to="/" style={{ textDecoration: 'none', color: 'white' }}>
                            HOME
                        </NavLink>
                        <NavLink className={({isActive}) => 'list-group-item' + (isActive ?' demo' : 'inactive')} to="/planners" style={{ textDecoration: 'none', color: 'white' }}>
                            PLANNERS
                        </NavLink>
                        <NavLink className={({isActive}) => 'list-group-item' + (isActive ?' demo' : 'inactive')} to="/profile" style={{ textDecoration: 'none', color: 'white' }}>
                            PROFILE
                        </NavLink>
                        <NavLink className={({isActive}) => 'list-group-item' + (isActive ?' demo' : 'inactive')} to={this.props.usertype == "ADMIN" ? "/register" : "/deniedAccess"} style={{ textDecoration: 'none', color: 'white' }}>
                            REGISTER
                        </NavLink>
                        <NavLink className={({isActive}) => 'list-group-item' + (isActive ?' demo' : 'inactive')} to="/logs" style={{ textDecoration: 'none', color: 'white' }}>
                            LOGS
                        </NavLink>
                    </Nav>  
                </Navbar.Collapse>
            </Navbar>
        )
    }

}