import React, {Component} from 'react'
import {Modal, Button, Row, Col, Form} from 'react-bootstrap'

export class EditEventModal extends Component {
    constructor(props){
        super(props);
        this.handleSubmit=this.handleSubmit.bind(this);
    }

    async handleSubmit(event){
        event.preventDefault();
        fetch(process.env.REACT_APP_API+'event/updateEvent', { //napisati metodu u kontroleru
            method:'PUT',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json',
                Authorization : "Bearer "+this.props.token
            },
            body:JSON.stringify({
                Text:event.target.Text.value,//izmijeniti
                Id:this.props.eventid,
                DateAndTime:event.target.DateAndTime.value,
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
                            Edit Event
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="EventText">
                                        <Form.Label>EventText</Form.Label>
                                        <Form.Control type="text" name="Text" required 
                                        defaultValue={this.props.eventtext}
                                        placeholder="EventText"/>
                                    </Form.Group>
                                    <Form.Group controlId="EventDate">
                                        <Form.Label>EventDate</Form.Label>
                                        <Form.Control type="text" name="DateAndTime" required 
                                        defaultValue={this.props.eventdate}
                                        placeholder="EventDate"/>
                                    </Form.Group>

                                    <Form.Group>
                                        <Button variant="primary" type="submit" onClick={this.props.onHide}>
                                            Update Event
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