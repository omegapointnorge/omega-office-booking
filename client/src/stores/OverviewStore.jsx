import { makeAutoObservable, makeObservable, observable } from "mobx";
import { createContext } from "react";
import { RootStore } from "./";

class OverviewStore {
    bookings = [
        {
            id: "1",
            name: "Hanne Panne",
        },
    ];

    constructor(rootStore: RootStore) {
    makeAutoObservable(this);
}

    addBooking() {
        this.bookings.push(
            {
            id: "2",
            name: "Mostafa Aziz",
        });
    }
}

export default OverviewStore;
