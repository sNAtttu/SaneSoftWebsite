import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import 'bootstrap/dist/css/bootstrap.css';
import Home from './scenes/home/Home';
import registerServiceWorker from './registerServiceWorker';
import './'

ReactDOM.render(<Home />, document.getElementById('root'));
registerServiceWorker();
