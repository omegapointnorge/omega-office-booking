import { makeAutoObservable } from "mobx";
import { rooms } from "../data/seats";

class OverviewStore {
  rooms = [];

  constructor() {
    this.initialize();
    makeAutoObservable(this);

    this.rooms = rooms;
  }

  async initialize() {
    try {
      const url = "/api/Room/rooms";

      const response = await fetch(url, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      });

      console.log(response);
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      const data = await response.json();
      this.setRooms(data); // Update MobX store with the fetched data
    } catch (error) {
      console.error(error);
    }
  }

  setRooms(data) {
    this.rooms = data.value;
  }
}

export default OverviewStore;
