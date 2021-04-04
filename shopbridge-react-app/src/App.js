import React from 'react';
//import logo from './logo.svg';
import './App.css';
import {BrowserRouter as Router, Route, Switch} from 'react-router-dom';
import ListInventoryComponent from './components/ListInventoryComponent';
import HeaderComponent from './components/HeaderComponent';
import FooterComponent from './components/FooterComponent';
import CreateProductComponent from './components/CreateProductComponent';
import ViewInventoryComponent from './components/ViewInventoryComponent';

function App() {
  return (
    <div>
        <Router>
              <HeaderComponent />
                <div className="container">
                    <Switch> 
                 
                          <Route path = "/" exact render={props => <ListInventoryComponent {...props} />}></Route>
                          <Route path = "/products"  render={props => <ListInventoryComponent {...props} />}></Route>
                          <Route path = "/add-product/:id"  render={props => <CreateProductComponent {...props} />}></Route>
                          <Route path = "/view-product/:id" render={props => <ViewInventoryComponent {...props} />}></Route>
                          {/* <Route path = "/update-employee/:id" component = {UpdateEmployeeComponent}></Route> */}
                    </Switch>
                </div>
              <FooterComponent />
        </Router>
    </div>
    
  );
}

export default App;