
import './App.css';
import { Table } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Home} from './Home.js';
import {List} from './List.js';
import {BrowserRouter, Route, NavLink, Routes} from 'react-router-dom';


function App() {

  return (
    <BrowserRouter>
    <div className="container">
      <h3 className='m-3 d-flex justify-content-center'>
        This is your personal home page
      </h3>
      
      <nav className="navbar navbar-expand-sm bg-light navbar-dark">
        <ul className="navbar-nav">
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/home">
              Home
            </NavLink>
            </li>
            <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/list">
              List
            </NavLink>
          </li>
        </ul>
      </nav>

      <Routes>
        <Route path="/home" element={<Home />}></Route>
        <Route path="/list" element={<List />}></Route>
      </Routes>

    </div>
    </BrowserRouter>
  );
}

export default App;
