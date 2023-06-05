import React, { Component } from 'react'
import { Modal, Button, Row, Col, Form } from 'react-bootstrap'
import { ConflictModal } from './ConflictModal'


export class EditPlannerModal extends Component {
    constructor(props) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.state = { conflictModalShow: false, planner: {} }
    }


    async handleSubmit(event) {
        event.preventDefault();

        fetch(process.env.REACT_APP_API + 'planner/getPlanner', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                Authorization: "Bearer " + this.props.token
            },
            body: JSON.stringify({
                PlannerId: this.props.plannerid
            })
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ planner: data })
                let date1 = new Date(this.props.time).toLocaleString();
                let date2 = new Date(data.time).toLocaleString();
                if (Date.parse(date1) < Date.parse(date2)) {
                    this.setState({ conflictModalShow: true });
                } else {
                    fetch(process.env.REACT_APP_API + 'planner/updatePlanner', {
                        method: 'PUT',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json',
                            Authorization: "Bearer " + this.props.token
                        },
                        body: JSON.stringify({
                            Id: this.props.plannerid,
                            PlannerName: event.target.PlannerName.value,
                            Token: this.props.token
                        })
                    })
                }
            })

    }

    render() {
        let conflictModalClose = () => this.setState({ conflictModalShow: false });
        const { planner } = this.state;
        return (
            <div className='container'>
                <Modal {...this.props} size="lg" aria-labelledby="contained-modal-title-vcenter" centered>
                    <Modal.Header closeButton>
                        <Modal.Title id="contained-modal-title-vcenter">
                            Edit Planner
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="PlannerName">
                                        <Form.Label>PlannerName</Form.Label>
                                        <Form.Control type="text" name="PlannerName" required
                                            defaultValue={this.props.plannername}
                                            placeholder="PlannerName" />
                                    </Form.Group>

                                    <Form.Group>
                                        <Button variant="primary" type="submit">
                                            Update Planner
                                        </Button>
                                    </Form.Group>
                                </Form>
                            </Col>
                        </Row>
                        <ConflictModal token={this.props.token} plannername={this.props.plannername}
                            show={this.state.conflictModalShow} onHide={conflictModalClose}
                            plannerid={this.props.plannerid}></ConflictModal>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="danger" onClick={this.props.onHide}>Close</Button>
                    </Modal.Footer>
                </Modal>
            </div>
        );
    }
}