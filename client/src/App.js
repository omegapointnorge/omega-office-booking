import logo from './logo.svg';
import './App.css';
import LoginPage from './pages/Login/LoginPage'
import  {Route, Routes} from 'react-router-dom';
import OverviewPage from "./pages/Overview/OverviewPage";
import {IsAuthenticated} from "./pages/Login/IsAuthenticated";
import {BigRoom} from "./pages/Rooms/BigRoom";

export default function App() {

    return (
        <div className="App">
            <Routes>
                <Route path="/" element={<IsAuthenticated />}></Route>
                <Route path="/login" element={<LoginPage />}></Route>
                <Route path="/overview" element={<OverviewPage />}></Route>
                <Route path='/bigroom' element={<BigRoom />}></Route>
            </Routes>
        </div>)
}
App.displayName = App.name
