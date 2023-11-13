import { makeAutoObservable } from "mobx";
import { rooms } from "../data/seats";

class OverviewStore {
  rooms = [];

  constructor() {
    makeAutoObservable(this);

    this.rooms = rooms;
  }
}

export default OverviewStore;
