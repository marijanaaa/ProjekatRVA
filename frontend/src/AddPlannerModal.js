import React, {Component} from 'react'
import {Modal, Button, Row, Col, Form} from 'react-bootstrap'

export class AddPlannerModal extends Component {
    constructor(props){
        super(props);
        this.handleSubmit=this.handleSubmit.bind(this);
    }


    async handleSubmit(event){
        event.preventDefault();
        fetch(process.env.REACT_APP_API+'planner/addNewPlanner', {
            method:'POST',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                PlannerName:event.target.Name.value,
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
                            Add Planner
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="Name">
                                        <Form.Label>PlannerName</Form.Label>
                                        <Form.Control type="text" name="Name" required placeholder="PlannerName"/>
                                    </Form.Group>
                                    <Form.Group>
                                        <Button variant="primary" type="submit">
                                            Add Planner
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