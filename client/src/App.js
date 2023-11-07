import logo from './logo.svg';
import './App.css';
import LoginPage from './Pages/Login/LoginPage'
import  {Route, Routes} from 'react-router-dom';
import OverviewPage from "./Pages/Overview/OverviewPage";
import {IsAuthenticated} from "./Pages/Login/IsAuthenticated";

export default function App() {
    return (
        <div className="App">
            <Routes>
                <Route path="/" element={<IsAuthenticated />}></Route>
                <Route path="/login" element={<LoginPage />}></Route>
                <Route path="/overview" element={<OverviewPage />}></Route>
            </Routes>
        </div>)
}
App.displayName = App.name
