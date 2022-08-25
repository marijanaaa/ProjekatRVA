import React, {Component} from "react";
import {Table} from 'react-bootstrap';
import {Button, ButtonToolbar} from 'react-bootstrap'
import {AddPlannerModal}  from './AddPlannerModal'
import {EditPlannerModal}  from './EditPlannerModal'
import {DetailsPlannerModal} from './DetailsPlannerModal'
import { SearchPlannerModal } from "./SearchPlannerModal";


export class Planners extends Component {

    constructor(props){
        super(props);
        this.state={planners:[], addModalShow:false, editModalShow:false, detailsModalShow:false,
        searchModalShow:false, date:null}
        
    }
    
    
    refreshList(){
        fetch(process.env.REACT_APP_API+'planner/getAllPlanners', {
            method:'POST',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json',
                Authorization : "Bearer "+this.props.tok
            },
            body:JSON.stringify({
                Token:this.props.tok
            })
        })
        .then(response=>response.json())
        .then(data=>{
            this.setState({ planners: data })
        }) 
    }

    componentDidMount() {
        this.refreshList();
    }

    componentDidUpdate(){
        this.refreshList();
    }

    deletePlanner(plannerid){
        if(window.confirm('Are you sure?')){
            fetch(process.env.REACT_APP_API+'planner/deletePlanner',{
                method:'DELETE',
                headers:{
                    'Accept':'application/json',
                    'Content-Type':'application/json',
                    Authorization : "Bearer "+this.props.tok
                },
                body:JSON.stringify({
                    Id:plannerid,
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
                    'Content-Type':'application/json',
                    Authorization : "Bearer "+this.props.tok
                },
                body:JSON.stringify({
                    PlannerId:plannerid,
                    Token:this.props.tok
                })
            })
        }
    }

    
    render() {
        const {planners, plannerid=0, plannername,date}=this.state;
        const t = this.props.tok;
        let addModalClose=()=>this.setState({addModalShow:false});
        let editModalClose=()=>this.setState({editModalShow:false});
        let detailsModalClose=()=>this.setState({detailsModalShow:false});
        let searchModalClose=()=>this.setState({searchModalShow:false});
        return (
            <div className="mt-5 d-flex justify-content-left">
                
                <ButtonToolbar>
                    <Button variant="primary" onClick={()=>this.setState({addModalShow:true})}>
                        Add Planner  
                    </Button>
                    <Button variant="info" onClick={()=>this.setState({searchModalShow:true})}>
                        Search 
                    </Button>
                    <AddPlannerModal show={this.state.addModalShow} onHide={addModalClose} token={this.props.tok}></AddPlannerModal>
                    <SearchPlannerModal show={this.state.searchModalShow} onHide={searchModalClose} token={this.props.tok}></SearchPlannerModal>
                </ButtonToolbar>

                 <Table className="mt-4" striped bordered hover size="sm"> 
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Options</th>
                        </tr>
                    </thead>
                    <tbody>
                        {planners.map(planner=>
                                <tr key={planner.id}>
                                    <td>{planner.id}</td>
                                    <td>{planner.plannerName}</td>
                                    <td>
                                        <ButtonToolbar>
                                            <Button className="mr-2" variant="info" 
                                            onClick={()=>this.setState({editModalShow:true, 
                                            plannerid:planner.id, plannername:planner.plannerName,
                                            date:new Date().toLocaleString()})}>
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
                                            token={this.props.tok}
                                            time={date}
                                            lastchange={planner.time}
                                            />
                                             <DetailsPlannerModal show={this.state.detailsModalShow}
                                            onHide={detailsModalClose} 
                                            plannerid={plannerid}
                                            token={this.props.tok}
                                            />

                                        </ButtonToolbar>
                                    </td>
                                </tr>
                        )}
                    </tbody>
                </Table>
                
            </div>
        );
    }
}