import { makeAutoObservable} from "mobx";
import {RootStore, useStores} from "./";
import toast from "react-hot-toast";
class RoomStore {
    seats = [
        { "id": 1, "seatId": 1, "isTaken": false },
        { "id": 2, "seatId": 2, "isTaken": false },
        { "id": 3, "seatId": 3, "isTaken": false },
        { "id": 4, "seatId": 4, "isTaken": false },
        { "id": 5, "seatId": 5, "isTaken": false },
        { "id": 6, "seatId": 6, "isTaken": false },
        { "id": 7, "seatId": 7, "isTaken": false },
        { "id": 8, "seatId": 8, "isTaken": false },
        { "id": 9, "seatId": 9, "isTaken": false },
        { "id": 10, "seatId": 10, "isTaken": false },
        { "id": 11, "seatId": 11, "isTaken": false },
        { "id": 12, "seatId": 12, "isTaken": false },
        { "id": 13, "seatId": 13, "isTaken": false },
        { "id": 14, "seatId": 14, "isTaken": false },
        { "id": 15, "seatId": 15, "isTaken": false }
    ];
    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
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
}

export default RoomStore;
