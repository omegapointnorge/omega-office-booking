# Getting Started

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

## Available Scripts

-> package.json

### `npm start`

Runs the app in the development mode.\
-> Follow README.md in project-folder "../README.md" - start docker container/connect to dev database - dotnet build && dotnet run
Open [http://localhost:44469](http://localhost:44469) to view it in your browser.

### Folder structure

src/
|- components/
|- common/ // Shared components
|- Component1/ // Other specific components
|- Component1Item/ // Subcomponents of specific components
|- core/
|- auth/ // Authentication related components and logic
|- routes/ // Directory for routing components
|- models/ // Data models or entities
|- services/ // Services for external API interactions
|- shared/
|- assets/ // Static files like images and fonts  
 |- hooks/ // Custom React hooks  
 |- utils/ // Utility functions
|- state/
|- context/ // Context files
|- stores/ // State management (e.g., mobx store)
|- App.js // Main application component
|- index.js // Entry point
