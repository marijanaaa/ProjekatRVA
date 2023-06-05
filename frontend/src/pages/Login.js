import '../App.css';
import profilePic from "../images/loginMan.png"
import emailPic from "../images/loginEmail.webp"
import passwordPic from "../images/loginPassword.png"
import React, { Component } from "react";

import { BrowserRouter, Redirect, Route, Switch } from 'react-router-dom'
import { Navigation } from '../Navigation'
import { Home } from '../pages/Home'
import { Planners } from '../pages/Planners'
import { Profile } from '../pages/Profile'
import { DeniedAccess } from '../pages/DeniedAccess'
import { Register } from '../pages/Register'
import { Logs } from '../pages/Logs'


export class Login extends Component {

    constructor(props) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.state = {
            user: {
                id: -1,
                name: '',
                lastname: '',
                usertype: '',
                token: '',
                isLoggedIn: false
            }
        }
    }

    async handleSubmit(event) {
        event.preventDefault();
        fetch(process.env.REACT_APP_API + 'authentication/login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                UserName: event.target.usrname.value,
                Password: event.target.pass.value
            })
        })
            .then(res => res.json())
            .then((data) => {
                if (event.target.usrname.value != "" && event.target.pass.value != "" && data.id != null) {
                    this.setState({
                        id: data.id, name: data.name,
                        lastname: data.lastName, usertype: data.userType, token: data.token, isLoggedIn: true
                    });
                }
            },
                (error) => {
                    alert('Failed');
                })
    }


    render() {
        const isLoggedIn = this.state.isLoggedIn;
        const userType = this.state.usertype;
        const token = this.state.token;
        return (
            <div>

                {
                    isLoggedIn ?
                        <BrowserRouter>
                            <div className="container">

                                <Navigation usertype={userType} />
                                <Switch>
                                    <Route path='/' component={Home} exact>
                                        <Home tok={token} />
                                    </Route>
                                    <Route path='/planners'>
                                        <Planners tok={token} />
                                    </Route>
                                    <Route path='/profile'>
                                        <Profile tok={token} />
                                    </Route>
                                    <Route path='/register'>
                                        <Register tok={token} />
                                    </Route>
                                    <Route path='/deniedAccess' component={DeniedAccess} />
                                    <Route path='/logs'>
                                        <Logs tok={token} />
                                    </Route>
                                </Switch>
                            </div>
                        </BrowserRouter>
                        :
                        <div className="main">
                            <div className="body">
                                <div>
                                    <div className="imgs">
                                        <div className="container-image">
                                            <img src={profilePic} className="profile" />
                                        </div>
                                    </div>

                                    <div>
                                        <h1>LOGIN PAGE</h1>
                                    </div>

                                    <form onSubmit={this.handleSubmit}>

                                        <div>
                                            <img src={emailPic} alt="email" className="email" />
                                            <input type="text" placeholder="USERNAME" className="username" id="usrname" />
                                        </div>
                                        <div className="in">
                                            <img src={passwordPic} alt="pass" className="email" />
                                            <input type="password" placeholder="PASSWORD" className="username" id="pass" />
                                        </div>
                                        <div className="submitButton">
                                            <input type="submit" value="Login" />
                                        </div>
                                    </form>

                                </div>
                            </div>
                        </div>
                }

            </div>
        )
    }
}