import { createContext, useContext } from "react";
import OverviewStore from "./OverviewStore";
export class RootStore {
    overviewStore: OverviewStore;

    constructor() {
        this.overviewStore = new OverviewStore(this);
    }
}

export const StoresContext = createContext(new RootStore());
export const useStores = () => useContext(StoresContext);
