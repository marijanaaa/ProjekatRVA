import React, {Component} from "react";
import {Table} from 'react-bootstrap';


export class Logs extends Component {

    constructor(props){
        super(props);
        this.state={logs:[]}
        
    }

    
    refreshList(){
        fetch(process.env.REACT_APP_API+'user/getLogs', {
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
            this.setState({ logs: data })
        }) 
    }

    componentDidMount() {
        this.refreshList();
    }

    componentDidUpdate(){
        this.refreshList();
    }

    
    render() {
        const {logs}=this.state;
        return (
            <div className="mt-5 d-flex justify-content-left">
                 <Table className="mt-4" striped bordered hover size="sm"> 
                    <thead>
                        <tr>
                            <th>Time</th>
                            <th>Type</th>
                            <th>Message</th>
                        </tr>
                    </thead>
                    <tbody>
                        {logs.map(log=>
                                <tr key={log.time}>
                                    <td>{log.time}</td>
                                    <td>{log.type}</td>
                                    <td>{log.message}</td>
                                </tr>
                        )}
                    </tbody>
                </Table>               
            </div>
        );
    }
}