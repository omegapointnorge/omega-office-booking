export enum Rooms {
  Small = "SMALL",
  Large = "LARGE",
  Sales = "SALES",
  Econ = "ECON",
  Hr = "HR",
  Oystein = "OYSTEIN",
}

export enum ZoomStatus {
  Small = "SMALL",
  Large = "LARGE",
  Sales = "SALES",
  EconOystein = "ECONOYSTEIN",
  Hr = "HR",
  ZoomedOut = "ZOOMEDOUT",
  Transition = "TRANSITION"
}

//TODO: CHECK IF ID small-room, large-room, sales-room needs to be changed
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
