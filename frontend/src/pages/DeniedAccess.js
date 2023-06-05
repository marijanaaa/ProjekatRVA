import React, {Component} from "react"; 

export class DeniedAccess extends Component {
 
    constructor(props) {
      super(props);
    }
    render(){
        return(
            <div><h1>You don't have permission for this page!</h1></div>
        );
    }
}