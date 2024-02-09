import React, { useState } from "react";
import Heading from "@common-components/Heading";
import { useAuthContext } from "@auth/useAuthContext";
import { observer } from "mobx-react-lite";
import OverviewMap from "@components/OverviewPage/OverviewMap/OverviewMap";
import SeatInfo from "@components/OverviewPage/OverviewSeatInfo/OverviewSeatInfo";
import DateSwitchButton from "@components/OverviewPage/OverviewDateSwitchButton/OverviewDateSwitchButton";
import bookingStore from "@stores/BookingStore";
import OverviewEventModeButton from "./OverviewEventModeButton/OverviewEventModeButton";
import { Calendar, DateObject } from "react-multi-date-picker";

const OverviewPage = observer(() => {
  const { user } = useAuthContext() ?? {};

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
        <div className="flex flex-col gap-10">
          <Heading title={welcomeTitle} subTitle={subTitle} />
          {isEventAdmin ? <OverviewEventModeButton /> : <DateSwitchButton />}
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
              <OverviewMap showSeatInfo={showSeatInfo} />
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
