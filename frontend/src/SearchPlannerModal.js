import React, {Component} from 'react'
import {Modal, Button, Row, Col, Form, ButtonToolbar} from 'react-bootstrap'
import {Table} from 'react-bootstrap';
import { EditPlannerModal } from './EditPlannerModal';
import { DetailsPlannerModal } from './DetailsPlannerModal';

export class SearchPlannerModal extends Component {
    constructor(props){
        super(props);
        this.state={planners:[], addModalShow:false, editModalShow:false, detailsModalShow:false}
        this.handleSubmit=this.handleSubmit.bind(this);
    }

    async handleSubmit(event){
        event.preventDefault();
        fetch(process.env.REACT_APP_API+'planner/searchPlanners', {
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
        .then(res=>res.json())
        .then(data=>{
            this.setState({ planners: data })
        }) 
    }



    deletePlanner(plannerid){
        if(window.confirm('Are you sure?')){
            fetch(process.env.REACT_APP_API+'planner/deletePlanner',{
                method:'DELETE',
                headers:{
                    'Accept':'application/json',
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    Id:plannerid,
                    Token:this.props.token
                })
            })
        }
    }

    duplicatePlanner(plannerid){
        if(window.confirm('Are you sure?')){
            fetch(process.env.REACT_APP_API+'planner/duplicatePlanner',{
                method:'POST',
                headers:{
                    'Accept':'application/json',
                    'Content-Type':'application/json'
                },
                body:JSON.stringify({
                    PlannerId:plannerid,
                    Token:this.props.token
                })
            })
        }
    }



    render(){
        const {planners, plannerid=0, plannername}=this.state;
        let editModalClose=()=>this.setState({editModalShow:false});
        let detailsModalClose=()=>this.setState({detailsModalShow:false});
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
                                            Search
                                        </Button>
                                    </Form.Group>
                                </Form>
                            </Col>
                        </Row>
                        <Table className="mt-4" striped bordered hover size="sm"> 
                    <thead>
                        <tr>
                            <th>PlannerName</th>
                            <th>Options</th>
                        </tr>
                    </thead>
                    <tbody>
                    {planners.map(planner=>
                                <tr key={planner.id}>
                                    <td>{planner.plannerName}</td>
                                    <td>
                                    <ButtonToolbar>
                                            <Button className="mr-2" variant="info" 
                                            onClick={()=>this.setState({editModalShow:true, 
                                            plannerid:planner.id, plannername:planner.plannerName})}>
                                                Edit
                                            </Button>

                                            
                                            <Button className="mr-2" variant="danger" onClick={()=>this.deletePlanner(planner.id)}>
                                                Delete
                                            </Button>

                                            <Button className="mr-2" variant="warning" 
                                            onClick={()=>this.setState({detailsModalShow:true, 
                                            plannerid:planner.id})}>
                                                Details
                                            </Button>

                                            <Button className="mr-2" variant="success" 
                                            onClick={()=>this.duplicatePlanner(planner.id)}>
                                                Duplication
                                            </Button>

                                            <EditPlannerModal show={this.state.editModalShow}
                                            onHide={editModalClose}
                                            plannerid={plannerid}
                                            plannername={plannername}
                                            token={this.props.token}
                                            />
                                             <DetailsPlannerModal show={this.state.detailsModalShow}
                                            onHide={detailsModalClose} 
                                            plannerid={plannerid}
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