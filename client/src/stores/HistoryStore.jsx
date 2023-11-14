import { makeAutoObservable } from "mobx";

class HistoryStore {
  myBookings = [];

  constructor() {
    makeAutoObservable(this);
  }
}

export default HistoryStore;
