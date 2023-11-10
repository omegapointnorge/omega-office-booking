import { makeAutoObservable} from "mobx";
import { RootStore } from "./";
import {rooms} from "../data/seats";

class HistoryStore {

    myBookings = [
    ];

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
    }
}

export default HistoryStore;
