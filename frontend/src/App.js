import './App.css';
import profilePic from "./images/loginMan.png"
import emailPic from "./images/loginEmail.webp"
import passwordPic from "./images/loginPassword.png"
import {Login} from "./Login"
import React from 'react';


function App() {
  return (  
    <React.Fragment>
      <div className="container">
        <Login />
      </div>
    </React.Fragment>
  );
}

export default App;
