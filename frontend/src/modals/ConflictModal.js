import React, {Component} from 'react'
import {Modal, Button, Row, Col, Form} from 'react-bootstrap'

export class ConflictModal extends Component {
    constructor(props){
        super(props);
        this.handleSubmit=this.handleSubmit.bind(this);
    }

    async handleSubmit(event){
        event.preventDefault();
        fetch(process.env.REACT_APP_API+'planner/updatePlanner', {
            method:'PUT',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                Id:this.props.plannerid,
                PlannerName:this.props.plannername,
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
                            CONFLICT
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Row>
                            <Button variant="primary" onClick={this.props.onHide}>
                                QUIT 
                            </Button>
                            <Button variant="info" onClick={this.handleSubmit}>
                                EDIT ANYWAY
                            </Button>
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