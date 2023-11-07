import logo from './logo.svg';
import './App.css';
import OfficeRoom from './components/OfficeRoom';

export default function App() {
     // Sample data representing seat availability
  const seats = [
    { seatNumber: 1, isAvailable: true },
    { seatNumber: 2, isAvailable: false },
    { seatNumber: 3, isAvailable: true },
    // Add more seats as needed
  ];
  
    return (
        <div className="App">
            <OfficeRoom>

            </OfficeRoom>
        </div>)
}
App.displayName = App.name
