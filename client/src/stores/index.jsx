import { createContext, useContext } from "react";
import OverviewStore from "./OverviewStore";
import RoomStore from "./RoomStore";

export class RootStore {
    overviewStore: OverviewStore;
    roomStore: RoomStore;

    constructor() {
        this.overviewStore = new OverviewStore(this);
        this.roomStore = new RoomStore(this);
    }
}

export const StoresContext = createContext(new RootStore());
export const useStores = () => useContext(StoresContext);
