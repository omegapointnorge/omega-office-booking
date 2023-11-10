import { makeAutoObservable} from "mobx";
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

    addBooking(name: string) {
        this.bookings.push(
            {
            id: "2",
            name: name ?? 'Nameless',
        });
    }

    deleteBooking() {
        this.bookings.pop();
    }
}

export default OverviewStore;
