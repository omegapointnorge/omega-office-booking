import logo from './logo.svg';
import './App.css';
import LoginPage from './Pages/Login/LoginPage'
import  {Route, Routes} from 'react-router-dom';
import OverviewPage from "./Pages/Overview/OverviewPage";

export default function App() {
    return (
        <div className="App">
            <LoginPage />
            <Routes>
                <Route path="/" element={<LoginPage />}></Route>
                <Route path="/overview" element={<OverviewPage />}></Route>
            </Routes>
        </div>)
}
App.displayName = App.name
