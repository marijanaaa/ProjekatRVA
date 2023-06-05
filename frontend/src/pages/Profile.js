import React, { Component } from "react";
import { Table } from 'react-bootstrap';
import { Button, ButtonToolbar } from 'react-bootstrap'
import { EditProfileModal } from '../modals/EditProfileModal'

export class Profile extends Component {

    constructor(props) {
        super(props);
        this.state = { profile: {}, editModalShow: false }

    }


    loadUser() {
        fetch(process.env.REACT_APP_API + 'user/getUser', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                Authorization: "Bearer " + this.props.token
            },
            body: JSON.stringify({
                Token: this.props.tok
            })
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ profile: data })
            })
    }

    componentDidMount() {
        this.loadUser();
    }

    componentDidUpdate() {
        this.loadUser();
    }




    render() {
        const { profile } = this.state;
        let editModalClose = () => this.setState({ editModalShow: false });
        return (
            <div className="mt-5 d-flex justify-content-left">
                <ButtonToolbar>
                    <Button variant="primary" onClick={() => this.setState({ editModalShow: true })}>
                        Edit Profile
                    </Button>
                    <EditProfileModal show={this.state.editModalShow} onHide={editModalClose}
                        token={this.props.tok} name={profile.name} lastname={profile.lastName}></EditProfileModal>
                </ButtonToolbar>
                <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                    </thead>
                    <tbody>
                        <tr><th>Name</th><th>{profile.name}</th></tr>
                        <tr><th>LastName</th><th>{profile.lastName}</th></tr>
                        <tr><th>Username</th><th>{profile.username}</th></tr>
                        <tr><th>UserType</th><th>{profile.userType}</th></tr>
                    </tbody>
                </Table>
            </div>
        );
    }
}