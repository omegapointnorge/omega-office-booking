import { createContext, useContext } from "react";
import OverviewStore from "./OverviewStore";
import RoomStore from "./RoomStore";
import HistoryStore from "./HistoryStore";

export class RootStore {
    overviewStore: OverviewStore;
    roomStore: RoomStore;
    historyStore: HistoryStore;

    constructor() {
        this.overviewStore = new OverviewStore(this);
        this.roomStore = new RoomStore(this);
        this.historyStore = new HistoryStore(this); 
    }
}

export const StoresContext = createContext(new RootStore());
export const useStores = () => useContext(StoresContext);
