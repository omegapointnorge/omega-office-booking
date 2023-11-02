
import './App.css';
import  React from 'react';
import  {Route, Routes} from 'react-router-dom';
import  {Desks} from "./components/Desks";
import  {PageLayout} from "./components/PageLayout";
import  {Homepage}from "./components/Home";
import  {NavigationBar}from "./components/NavigationBar.jsx";

export default function App() {
    return (
        <>
        <NavigationBar />
        <Routes>
        <Route path="/" element={<Homepage />}></Route>
        <Route path="/Booking" element={<PageLayout />}></Route>
        <Route path="/Overview" element={<Desks />}></Route>
    </Routes>
    </>)

}
App.displayName = App.name