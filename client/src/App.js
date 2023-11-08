import logo from './logo.svg';
import './App.css';
import LoginPage from './Pages/Login/LoginPage'
import  {Route, Routes} from 'react-router-dom';
import OverviewPage from "./Pages/Overview/OverviewPage";
import { IsAuthenticated } from "./Pages/Login/IsAuthenticated";
import {BigRoom} from "./pages/Rooms/BigRoom";

import {
    QueryClient,
    QueryClientProvider,
    useQuery,
} from '@tanstack/react-query'

const queryClient = new QueryClient()
export default function App() {

    return (
        <QueryClientProvider client={queryClient}>
        <div className="App">
            <Routes>
                <Route path="/" element={<IsAuthenticated />}></Route>
                <Route path="/login" element={<LoginPage />}></Route>
                <Route path="/overview" element={<OverviewPage />}></Route>
                <Route path='/bigroom' element={<BigRoom />}></Route>
            </Routes>
        </div>
        </QueryClientProvider>)
}
App.displayName = App.name
