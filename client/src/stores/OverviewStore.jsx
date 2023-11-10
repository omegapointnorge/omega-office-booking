import { makeAutoObservable} from "mobx";
import { RootStore } from "./";

class OverviewStore {
    
    bookings = [
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
