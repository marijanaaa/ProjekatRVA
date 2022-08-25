import React, {Component} from "react";
import {Button} from 'react-bootstrap'


export class Home extends Component {
    constructor(props){
        super(props);
        this.handleSubmit=this.handleSubmit.bind(this);
    }

    async handleSubmit(event){
        if(window.confirm('Are you sure?')){
            event.preventDefault();
            fetch(process.env.REACT_APP_API+'user/logout', {
                method:'POST',
                headers:{
                    'Accept':'application/json',
                    'Content-Type':'application/json',
                    Authorization : "Bearer "+this.props.tok
                },
                body:JSON.stringify({
                    Token:this.props.tok
                }),
                redirect:"manual"
            })
            .then(res=>res.json())
                window.location.href = '/login';
            
            
        }
        
    }
    render() {
        return (
            <div className="mt-5 d-flex justify-content-left">
                <h1>Home</h1><br/>
                    <Button variant="primary" onClick={this.handleSubmit}>
                            LOG OUT 
                    </Button>
            </div>
        );
    }
}