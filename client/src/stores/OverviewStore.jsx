import { makeAutoObservable} from "mobx";
import { RootStore } from "./";
import {rooms} from "../data/seats";

class OverviewStore {
    
    rooms = [
    ];

    constructor(rootStore: RootStore) {
        makeAutoObservable(this);
    
        this.rooms = rooms;
    
    }
}

export default OverviewStore;
