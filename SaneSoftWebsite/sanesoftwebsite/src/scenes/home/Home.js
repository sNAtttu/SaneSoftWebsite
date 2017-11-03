import React, { Component } from 'react';
import './Home.css';
import Commands from './components/commandbar/Commands';
import Banner from './components/banner/Banner';
import 'bootstrap/dist/css/bootstrap.css';

class Home extends Component {

  render() {
    return (
      <div>
        <div className="applicationTopBar">
          <span>Website.exe</span>
        </div>
        <div className="content">
          <Banner />
          <Commands />
        </div>
      </div>
    );
  }
}

export default Home;
