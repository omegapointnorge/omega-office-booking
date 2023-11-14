export const bigRoomSeats = [
    { "id": 1, "seatId": 1, "isTaken": false },
    { "id": 2, "seatId": 2, "isTaken": false },
    { "id": 3, "seatId": 3, "isTaken": false },
    { "id": 4, "seatId": 4, "isTaken": false },
    { "id": 5, "seatId": 5, "isTaken": false },
    { "id": 6, "seatId": 6, "isTaken": false },
    { "id": 7, "seatId": 7, "isTaken": false },
    { "id": 8, "seatId": 8, "isTaken": false },
    { "id": 9, "seatId": 9, "isTaken": false },
    { "id": 10, "seatId": 10, "isTaken": false },
    { "id": 11, "seatId": 11, "isTaken": false },
    { "id": 12, "seatId": 12, "isTaken": false },
    { "id": 13, "seatId": 13, "isTaken": false },
    { "id": 14, "seatId": 14, "isTaken": false },
    { "id": 15, "seatId": 15, "isTaken": false }
]

export const smallRoomSeats = [
    { "id": 1, "seatId": 1, "isTaken": false },
    { "id": 2, "seatId": 2, "isTaken": false },
    { "id": 3, "seatId": 3, "isTaken": false },
    { "id": 4, "seatId": 4, "isTaken": false },
    { "id": 5, "seatId": 5, "isTaken": false }
]

export const rooms = [
    {
        "id": 1,
        "name": "Store rommet",
        "capacity": 15,
        "office": "OSLO",
        "image": "big-room.png",
        "route": "bigroom",
        "seats": [
            bigRoomSeats
        ]
    },
    {
        "id": 2,
        "name": "Lille rommet",
        "capacity": 5,
        "office": "OSLO",
        "image": "small-room.png",
        "route": "smallroom",
        "seats": [
            smallRoomSeats
        ]
    },
    {
        "id": 3,
        "name": "Salg",
        "capacity": 6,
        "office": "OSLO",
        "image": "small-room.png",
        "route": "salg",
        "seats": [
            smallRoomSeats
        ]
    }
]
