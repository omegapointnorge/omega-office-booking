import React, { useState } from "react";
import Heading from "@common-components/Heading";
import { useAuthContext } from "@auth/useAuthContext";
import { observer } from "mobx-react-lite";
import OverviewMap from "@components/OverviewPage/OverviewMap/OverviewMap";
import SeatInfo from "@components/OverviewPage/OverviewSeatInfo/OverviewSeatInfo";
import DateSwitchButton from "@components/OverviewPage/OverviewDateSwitchButton/OverviewDateSwitchButton";

const OverviewPage = observer(() => {
  const { user } = useAuthContext() ?? {};

  const userName = user?.claims?.find((claim) => claim.key === "name")?.value;
  const welcomeTitle = `Velkommen ${userName || ""}`; // Handle undefined userName
  const subTitle = "Vennligst velg rom for å booke";

  const [showModal, setShowModal] = useState(false);
  const [selectedSeatId, setSelectedSeatId] = useState(null);

  const showSeatInfo = (seatId) => {
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
          <DateSwitchButton />
          <div className="flex flex-row gap-24">
            <OverviewMap showSeatInfo={showSeatInfo} />
          </div>
        </div>
      </div>
      {showModal && (
        <SeatInfo
          onClose={() => setShowModal(false)}
          selectedSeatId={selectedSeatId}
        />
      )}
    </>
  );
});

export default OverviewPage;
