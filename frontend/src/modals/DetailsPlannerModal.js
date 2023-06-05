import React, { Component } from 'react'
import { Modal, Button, Row, Col, Form } from 'react-bootstrap'
import { Table } from 'react-bootstrap';
import { ButtonToolbar } from 'react-bootstrap'
import { AddEventModal } from './AddEventModal';
import { EditEventModal } from './EditEventModal';

export class DetailsPlannerModal extends Component {
    constructor(props) {
        super(props);
        this.state = { events: [], addEventModalShow: false, editEventModalShow: false }
    }


    refreshList() {
        fetch(process.env.REACT_APP_API + 'event/getAllEvents?plannerId=' + this.props.plannerid, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                Authorization: "Bearer " + this.props.token
            }
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ events: data })
            });
    }

    componentDidMount() {
        this.refreshList();
    }

    componentDidUpdate() {
        this.refreshList();
    }

    deleteEvent(eventid) {
        if (window.confirm('Are you sure?')) {
            fetch(process.env.REACT_APP_API + 'event/deleteEvent?eventId=' + eventid, {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    Authorization: "Bearer " + this.props.token
                }
            })
        }
    }


    render() {
        const { events, eventid, eventtext, eventdate } = this.state;
        let addEventModalClose = () => this.setState({ addEventModalShow: false });
        let editEventModalClose = () => this.setState({ editEventModalShow: false });
        return (
            <div className='container'>
                <Modal {...this.props} size="lg" aria-labelledby="contained-modal-title-vcenter" centered>
                    <Modal.Header closeButton>
                        <Modal.Title id="contained-modal-title-vcenter">
                            Events
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <ButtonToolbar>
                            <Button variant="primary" onClick={() => this.setState({ addEventModalShow: true })}>
                                Add event
                            </Button>
                            <AddEventModal show={this.state.addEventModalShow} onHide={addEventModalClose}
                                token={this.props.token} idplanner={this.props.plannerid}></AddEventModal>
                        </ButtonToolbar>
                        <Table className="mt-4" striped bordered hover size="sm">
                            <thead>
                                <tr>
                                    <th>Text</th>
                                    <th>DateAndTime</th>
                                    <th>Options</th>
                                </tr>
                            </thead>
                            <tbody>
                                {events.map(event =>
                                    <tr key={event.id}>
                                        <td>{event.text}</td>
                                        <td>{event.dateAndTime}</td>
                                        <td>
                                            <ButtonToolbar>
                                                <Button className="mr-2" variant="info"
                                                    onClick={() => this.setState({
                                                        editEventModalShow: true,
                                                        eventid: event.id, eventtext: event.text,
                                                        eventdate: event.dateAndTime
                                                    })}>
                                                    Edit
                                                </Button>


                                                <Button className="mr-2" variant="danger" onClick={() => this.deleteEvent(event.id)}>
                                                    Delete
                                                </Button>

                                                <EditEventModal show={this.state.editEventModalShow}
                                                    onHide={editEventModalClose}
                                                    eventid={eventid}
                                                    eventtext={eventtext}
                                                    eventdate={eventdate}
                                                    token={this.props.token}
                                                />

                                            </ButtonToolbar>

                                        </td>
                                    </tr>
                                )}
                            </tbody>
                        </Table>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="danger" onClick={this.props.onHide}>Close</Button>
                    </Modal.Footer>
                </Modal>
            </div>
        );
    }
}