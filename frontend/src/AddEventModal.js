import React, {Component} from 'react'
import {Modal, Button, Row, Col, Form} from 'react-bootstrap'

export class AddEventModal extends Component {
    constructor(props){
        super(props);
        this.handleSubmit=this.handleSubmit.bind(this);
    }

    async handleSubmit(event){
        event.preventDefault();
        fetch(process.env.REACT_APP_API+'event/addEvent', {
            method:'POST',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json',
                Authorization : "Bearer "+this.props.token
            },
            body:JSON.stringify({
                Text:event.target.Text.value,
                DateAndTime:event.target.DateAndTime.value,
                PlannerId:this.props.idplanner,
            })
        })
    }

    render(){
        return(
            <div className='container'>
                <Modal {...this.props} size="lg" aria-labelledby="contained-modal-title-vcenter" centered>
                    <Modal.Header closeButton>
                        <Modal.Title id="contained-modal-title-vcenter">
                            Add Event
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="Text">
                                        <Form.Label>Text</Form.Label>
                                        <Form.Control type="text" name="Text" required placeholder="EventText"/>
                                    </Form.Group>
                                    <Form.Group controlId="DateAndTime">
                                        <Form.Label>DateAndTime</Form.Label>
                                        <Form.Control type="text" name="DateAndTime" required placeholder="EventDateAndTime"/>
                                    </Form.Group>
                                    <Form.Group>
                                        <Button variant="primary" type="submit" onClick={this.props.onHide}>
                                            Add Event
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