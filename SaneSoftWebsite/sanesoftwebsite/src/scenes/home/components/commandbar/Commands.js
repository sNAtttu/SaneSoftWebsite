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
      let currentCommands = self.state.previousCommands;
      currentCommands.push(
        {
          "Command": userInput,
          "Answer": json.answer
        }
      );
      self.setState({previousCommands: currentCommands});
      $('#commandTextInput').val("");
      $('html, body').animate({
        scrollTop: $("#commandTextInput").offset().top
    }, 1000);
    });
  }

  componentDidMount(){
    var self = this;
    $('#commandTextInput').focus();
    $("#commandTextInput").keyup(function(event) {
      if (event.keyCode === 13) {
        self.getAnswerForCommand(event.target.value);
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
          <div className="col-sm-12">
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
              <div className="col-xs-11">
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
