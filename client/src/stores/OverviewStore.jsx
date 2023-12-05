import { makeAutoObservable } from "mobx";
import { Room } from "../domain/room";

class OverviewStore {
  rooms = [];

  constructor() {
    this.initialize();
    makeAutoObservable(this);
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

      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      const data = await response.json();

      this.setRooms(data);
    } catch (error) {
      console.error(error);
    }
  }

  setRooms(data) {
    this.rooms = data.value.map(
      (room) => new Room(room.id, room.name, room.seats)
    );
  }

  getRouteName(route) {
    return route.replace(/\s+/g, "-").toLowerCase();
  }
}

const overviewStore = new OverviewStore();
export default overviewStore;
