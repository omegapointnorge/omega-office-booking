import React, { useState } from "react";
import Heading from "@common-components/Heading";
import { useAuthContext } from "@auth/useAuthContext";
import { observer } from "mobx-react-lite";
import OverviewMap from "@components/OverviewPage/OverviewMap/OverviewMap";
import SeatInfo from "@components/OverviewPage/OverviewSeatInfo/OverviewSeatInfo";
import DateSwitchButton from "@components/OverviewPage/OverviewDateSwitchButton/OverviewDateSwitchButton";
import bookingStore from '@stores/BookingStore';

const OverviewPage = observer(() => {
  const { user } = useAuthContext() ?? {};

  const { bookEventMode } = bookingStore;
  const userName = user.claims.userName
  const isEventAdmin = user.claims.role === "EventAdmin"

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

  return (
  <>
    <div className="justify-center items-center flex flex-col inset-0">
      <div className="flex flex-col gap-10">
        <Heading title={welcomeTitle} subTitle={subTitle} />
        {isEventAdmin ? (
          <button onClick={() => bookingStore.setBookEventMode(!bookEventMode)}>
            {bookEventMode ? "Cancel new event booking" : "Book event"}
          </button>
        ) : (
          <DateSwitchButton />
        )}
        <div className="flex flex-row gap-24">
          <OverviewMap showSeatInfo={showSeatInfo} />
        </div>
      </div>
    </div>
    {showModal && selectedSeatId !== undefined && ( // Check for undefined selectedSeatId
      <SeatInfo
        onClose={() => setShowModal(false)}
        selectedSeatId={selectedSeatId}
      />
    )}
  </>
);

});

  
export default OverviewPage;
