import logo from './logo.svg';
import './App.css';
import React from "react";
import {useLocation} from "react-router-dom";

function App() {
  const location = useLocation();
  const login = async (event: React.FormEvent) => {
    event.preventDefault();
    window.location.href = 'client/account/login';
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
            <button
                type="submit"
                className="group relative flex w-full justify-center rounded-md border border-transparent bg-green-600 py-2 px-4 text-sm font-medium text-white hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 "
            >
              <span className="absolute inset-y-0 left-0 flex items-center pl-3"></span>
              Logg in
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default App;
