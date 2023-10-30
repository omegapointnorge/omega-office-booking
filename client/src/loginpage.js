import logo from './logo.svg';
import './App.css';
import React from "react";
import {useLocation} from "react-router-dom";

function LoginPage() {
    const location = useLocation();
    const login = async (event: React.FormEvent) => {
        event.preventDefault();
        window.location.href = 'api/Account/Login';
    };

    return (
        <div className="App">
            <header className="App-header">
                <img src={logo} className="App-logo" alt="logo" />
                <p>
                    Edit <code>src/App.js</code> and save to reload.
                </p>
                <a
                    className="App-link"
                    href="https://reactjs.org"
                    target="_blank"
                    rel="noopener noreferrer"
                >
                    Learn React
                </a>
            </header>
            <div>
                <form className="mt-8 space-y-6" onSubmit={login}>
                    <div className="flex min-h-full items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
                        <a href="https://localhost:5001/api/Account/Login">click here to login</a>;
                    </div>
                    <div>
                        <button onClick={login}>Login with LOCATION</button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default LoginPage;
