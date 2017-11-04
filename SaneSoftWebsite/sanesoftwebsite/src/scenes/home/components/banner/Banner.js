import React, { Component } from 'react';
import './Banner.css';

class Banner extends Component {
  
  render() {
    return (
      <div className="container-fluid">
          <div className="row">
              <div className="col-sm-12">
                <div className="banner">
                    <h1 className="banner-title">SaneSoftware</h1>        
                </div>
              </div>
          </div>
          <div className="row">
            <div className="col-sm-12 col-md-6 col-lg-6">
              <div className="banner-information">
                <p className="CMDText-small"> Welcome to the CMD Version of SaneSoftwares website. </p>
                <p className="CMDText-small"> Start using the website by typing commands like "MAIL" or "PHONE". </p>
              </div>     
            </div>
          </div>
      </div>
    );
  }
}

export default Banner;
