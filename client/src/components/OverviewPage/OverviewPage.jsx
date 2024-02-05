import React, {useState} from 'react';
import Heading from '@common-components/Heading';
import { useAuthContext } from '@auth/useAuthContext';
import { observer } from 'mobx-react-lite';
import OverviewMap from '@components/OverviewPage/OverviewMap/OverviewMap';
import SeatInfo from '@components/OverviewPage/OverviewSeatInfo/OverviewSeatInfo';
import EventBookingInfo from '@components/OverviewPage/OverviewSeatInfo/OverviewEventInfo';
import DateSwitchButton from '@components/OverviewPage/OverviewDateSwitchButton/OverviewDateSwitchButton';
import bookingStore from '@stores/BookingStore';

const OverviewPage = observer(() => {
  const { user } = useAuthContext() ?? {};
  const {eventAdminSeletedBookings } = bookingStore;
  const userName = user?.claims?.find(claim => claim.key === 'name')?.value;
  const welcomeTitle = `Velkommen ${userName || ''}`; // Handle undefined userName
  const subTitle = 'Vennligst velg rom for å booke';

  const [showModal, setShowModal] = useState(false);
  const [eventShowModal, setEventShowModal] = useState(false);
  const [selectedSeatId, setSelectedSeatId] = useState(null);
  const [selectedSeatIds, setSelectedSeatIds] = useState(null);


  const showSeatInfo = (seatId, mode) => {
    try {
      if(mode === "NormalMode"){
        setSelectedSeatId(Number(seatId));
        setShowModal(true);
      } else if(mode === "EventBookingMode"){
        setSelectedSeatIds(seatId);
        setEventShowModal(true);
      }
      
    }
    catch (e) {
      console.error(e)
    }
  };  
  const onEventBookingInfoClose = () => {
    setEventShowModal(false);
    bookingStore.resetSelectedSeatId();
  }

  return (
    <>
      <div className="justify-center items-center flex flex-col inset-0">
        <div className="flex flex-col gap-10">
          <Heading title={welcomeTitle} subTitle={subTitle} />
          <DateSwitchButton />
          <div className="flex flex-row gap-24">
            <OverviewMap showSeatInfo={showSeatInfo}/>
          </div>
        </div>
      </div>
      {showModal && <SeatInfo onClose={() => setShowModal(false)} selectedSeatId={selectedSeatId}/>}
      {eventShowModal && <EventBookingInfo onClose={() => onEventBookingInfoClose()} selectedSeatIds={selectedSeatIds}/>}
    </>
  );
});

export default OverviewPage;
