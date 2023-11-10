import { makeAutoObservable} from "mobx";
import {RootStore, useStores} from "./";
import toast from "react-hot-toast";
import {bigRoomSeats, rooms} from "../data/seats";
class RoomStore{
    
    rooms = [];
    
    seats = [
        
    ];
    
    openDialog = false;
    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
        
        this.seats = bigRoomSeats;
    }

    bookSeat(id, isTaken) {
        const seatToUpdate = this.seats.find(seat => seat.id === id);

        if (seatToUpdate) {
            seatToUpdate.isTaken = isTaken;
            toast.success('Booked seat')
        } else {
            console.log(`Seat with ID ${id} not found.`);
            toast.error('Seat not found');
        }
    }
    
    /* DIALOG */
    handleOpenDialog = () => {
        this.openDialog = !this.openDialog;
    };

    handleCloseDialog = () => {
        this.openDialog = !this.openDialog;
    };
}

export default RoomStore;
