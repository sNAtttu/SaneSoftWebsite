import React, { Component } from 'react';
import './Commands.css';
import $ from 'jquery';

class Commands extends Component {
  
  constructor(){
    super();

    this.state = {command: "", previousCommands: []}
    this.handleChange = this.handleChange.bind(this);

  }
  
  handleChange(event){
    this.setState({command: event.target.value})
  }

  getAnswerForCommand(userInput){
    $('#commandTextInput').val("Bot is thinking..");
    var self = this;
    fetch('http://sanesoftwebsitebotapi.azurewebsites.net/api/commands', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        Command: userInput,
        Answer: '',
      })
    }).then((response) => {
      return response.json();
    }).then((json) => {
      self.AddCommandToScreen({"Command": userInput, "Answer": json.answer});
    });
  }

  AddCommandToScreen(command){
    let currentCommands = this.state.previousCommands;
    currentCommands.push(
      {
        "Command": command.Command,
        "Answer": command.Answer
      }
    );
    this.setState({previousCommands: currentCommands});
    $('#commandTextInput').val("");
    $('html, body').animate({
      scrollTop: $("#commandTextInput").offset().top
  }, 1000);
  }

  ValidateInput(userInput){
    let message = "Please provide an input before smashing Enter.";
    if(!userInput){   
      this.AddCommandToScreen({"Command": userInput, "Answer": message})
      return false;
    }
    if(userInput.length <= 2){
      message = "Don't spam nonsense.";
      this.AddCommandToScreen({"Command": userInput, "Answer": message})
      return false;
    }
    if(userInput.toLowerCase() === "santeri"){
      window.location.replace("https://fi.wikipedia.org/wiki/Santeri");
      return false;
    }
    return true;
  }

  componentDidMount(){
    var self = this;
    
    $('#commandTextInput').focus();
    $("#commandTextInput").keyup(function(event) {
      if (event.keyCode === 13) {

        if(self.ValidateInput(event.target.value)){
          self.getAnswerForCommand(event.target.value);
        }    
      }
  });
  }

  render() {

    const commandList = this.state.previousCommands.map((command, index) =>
    <li key={index} className="CMDTEXT">
    <span className="commandItem">{command.Command}</span>
    <span className="commandAnswer">{command.Answer}</span>
    </li>
    );

    return (
      <div className="container-fluid">
        <div className="row">
          <div className="col-sm-12 col-md-6 col-lg-6">
            <ul>
              {commandList}
            </ul>
          </div>
        </div>
        <div className="row">
            <div className="commandbarContainer">
              <div className="col-xs-1 visible-xs">
                <span className="commandCaret">></span>
              </div>
              <div className="col-sm-12 col-md-6 col-lg-6">
                <span className="commandCaret visible-sm visible-md visible-lg">></span>
                <input id="commandTextInput" type="text" name="command" 
                autoCorrect="off" 
                autoComplete="off"
                value={this.state.command.toUpperCase()} 
                onChange={this.handleChange} />
              </div>
            </div>
        </div>
      </div>

    );
  }
}

export default Commands;
