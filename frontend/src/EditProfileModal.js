import React, {Component} from 'react'
import {Modal, Button, Row, Col, Form} from 'react-bootstrap'

export class EditProfileModal extends Component {
    constructor(props){
        super(props);
        this.handleSubmit=this.handleSubmit.bind(this);
        this.state={conflictModalShow:false}
    }

    async handleSubmit(event){
        event.preventDefault();
        fetch(process.env.REACT_APP_API+'user/editProfile', {
            method:'PUT',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json',
                Authorization : "Bearer "+this.props.token
            },
            body:JSON.stringify({
                Name:event.target.Name.value,
                LastName:event.target.LastName.value,
                Token:this.props.token
            })
        })
    }

    render(){
        return(
            <div className='container'>
                
                <Modal {...this.props} size="lg" aria-labelledby="contained-modal-title-vcenter" centered>
                    <Modal.Header closeButton>
                        <Modal.Title id="contained-modal-title-vcenter">
                            Edit Profile
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="Name">
                                        <Form.Label>Name</Form.Label>
                                        <Form.Control type="text" name="Name" required placeholder="Name"
                                        defaultValue={this.props.name}/>
                                    </Form.Group>
                                    <Form.Group controlId="LastName">
                                        <Form.Label>LastName</Form.Label>
                                        <Form.Control type="text" name="LastName" required placeholder="LastName"
                                        defaultValue={this.props.lastname}/>
                                    </Form.Group>
                                    <Form.Group>
                                        <Button variant="primary" type="submit" onClick={this.props.onHide}>
                                            Edit Profile
                                        </Button>
                                    </Form.Group>
                                </Form>
                            </Col>
                        </Row>

                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="danger" onClick={this.props.onHide}>Close</Button>
                    </Modal.Footer>
                </Modal>
            </div>
        );
    }
}