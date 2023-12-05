import { makeAutoObservable } from "mobx";

class HistoryStore {
  myBookings = [];

  constructor() {
    makeAutoObservable(this);
  }
}

const historyStore = new HistoryStore();
export default historyStore;
