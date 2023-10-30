import React from 'react';
import {Route, Routes} from 'react-router-dom';
import LoginPage from "./loginpage";
import Overview from "./overview";

export default function App() {
  return (
        <Routes>
            <Route path="/" element={<LoginPage />}></Route>
            <Route path="/login" element={<LoginPage />}></Route>
            <Route path="/overview" element={<Overview />}></Route>
        </Routes>
  );
}
App.displayName = App.name
