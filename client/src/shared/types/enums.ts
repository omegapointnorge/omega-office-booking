export enum Rooms {
  Small = "SMALL",
  Large = "LARGE",
  Econ = "ECON",
  Marie = "MARIE",
  Oystein = "OYSTEIN",
}

export enum ZoomStatus {
  Small = "SMALL",
  Large = "LARGE",
  EconOystein = "ECONOYSTEIN",
  Marie = "Marie",
  ZoomedOut = "ZOOMEDOUT",
  Transition = "TRANSITION"
}

//TODO: CHECK IF ID small-room, large-room needs to be changed
export enum DatePressed {
  Today = "TODAY",
  NextWorkDay = "NEXTWORKDAY",
}

export enum UserRole {
  EventAdmin = "EventAdmin",
  User = "User",
}

export enum ApiStatus {
  Idle = "IDLE",
  Pending = "PENDING",
  Error = "ERROR",
  Success = "SUCCESS",
}
