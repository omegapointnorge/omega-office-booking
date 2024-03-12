import React, { useEffect, useState } from "react";
import { Heading } from "@common-components/Heading";
import { useAuthContext } from "@auth/useAuthContext";
import { observer } from "mobx-react-lite";
import OverviewMap from "@components/OverviewPage/Map";
import { SeatInfo } from "@components/OverviewPage/SeatInfo";
import { DateSwitchButton } from "@components/OverviewPage/DateSwitchButton";
import bookingStore from "@stores/BookingStore";
import { EventModeButton } from "./EventModeButton";
import { Calendar, DateObject } from "react-multi-date-picker";

const OverviewPage = observer(() => {
  const { user } = useAuthContext() ?? {};

  useEffect(() => {
    bookingStore.initialize();
  }, []);

  const userName = user.claims.userName;
  const isEventAdmin = user.claims.role === "EventAdmin";

  const welcomeTitle = `Velkommen ${userName || ""}`; // Handle undefined userName
  const subTitle = "Vennligst velg rom for å booke";

  const [showModal, setShowModal] = useState(false);
  const [selectedSeatId, setSelectedSeatId] = useState<number>();

  const showSeatInfo = (seatId: string) => {
    try {
      setSelectedSeatId(Number(seatId));
      setShowModal(true);
    } catch (e) {
      console.error(e);
    }
  };

  const dateObjectToDate = (dateObject: DateObject) => {
    return new Date(
      dateObject.year,
      dateObject.month.number - 1,
      dateObject.day
    );
  };

  return (
    <>
      <div className="flex flex-col justify-center items-center h-full">
        <div className="flex flex-col gap-2">
          <Heading title={welcomeTitle} subTitle={subTitle} />
          {isEventAdmin ? <EventModeButton /> : <DateSwitchButton />}
          <div className="flex justify-center">
            {bookingStore.bookEventMode &&
            bookingStore.isEventDateChosen === false ? (
              <Calendar
                onChange={(newDate: DateObject) =>
                  bookingStore.handleEventDate(dateObjectToDate(newDate))
                }
                minDate={new Date()}
                multiple={false}
              />
            ) : (
              <div>
                {/* {bookingStore.bookEventMode && <ToggleSwitch></ToggleSwitch>} */}
                <OverviewMap showSeatInfo={showSeatInfo} />
              </div>
            )}
          </div>
        </div>
      </div>
      {showModal && selectedSeatId !== undefined && (
        <SeatInfo
          onClose={() => setShowModal(false)}
          selectedSeatId={selectedSeatId}
        />
      )}
    </>
  );
});

export default OverviewPage;