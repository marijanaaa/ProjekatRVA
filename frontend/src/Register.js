import React, {Component} from "react";
import {Table} from 'react-bootstrap';
import {Button, ButtonToolbar} from 'react-bootstrap'
import {Form} from 'react-bootstrap'

export class Register extends Component {

    constructor(props){
        super(props);
        this.handleSubmit=this.handleSubmit.bind(this);
    }

    async handleSubmit(event){
        event.preventDefault();
        fetch(process.env.REACT_APP_API+'user/registerUser', {
            method:'POST',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                Name:event.target.Name.value,
                LastName:event.target.LastName.value,
                Username:event.target.Username.value,
                Password:event.target.Password.value,
                UserType:event.target.UserType.value,
                Token:this.props.tok
            })
        })
        .then(res=>res.json());
    }

    render() {
        return (
            <div className="mt-5 d-flex justify-content-left">

                
    <Form onSubmit={this.handleSubmit}>
      <Form.Group className="mb-3" controlId="Name">
        <Form.Label>Name</Form.Label>
        <Form.Control type="text" placeholder="Enter name" />
      </Form.Group>

      <Form.Group className="mb-3" controlId="LastName">
        <Form.Label>LastName</Form.Label>
        <Form.Control type="text" placeholder="Enter last name" />
      </Form.Group>

      <Form.Group className="mb-3" controlId="Username">
        <Form.Label>Username</Form.Label>
        <Form.Control type="text" placeholder="Enter username" />
      </Form.Group>

      <Form.Group className="mb-3" controlId="Password">
        <Form.Label>Password</Form.Label>
        <Form.Control type="password" placeholder="Enter password" />
      </Form.Group>
    
      <Form.Group className="mb-3" controlId="UserType">
        <Form.Label>UserType</Form.Label>
        <Form.Select aria-label="Default select example">
            <option value="ADMIN" defaultValue={"ADMIN"}>ADMIN</option>
            <option value="GUEST">GUEST</option>
        </Form.Select>
      </Form.Group>


      
      <Button variant="primary" type="submit">
        Submit
      </Button>
    </Form>
            </div>
        );
    }
}