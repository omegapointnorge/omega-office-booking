import React, {useState} from 'react';
import Heading from '../../components/Heading';
import Booking from '../../domain/booking'
import { useAuthContext } from '../../api/useAuthContext';
import { observer } from 'mobx-react-lite';
import overviewStore from '../../stores/OverviewStore';
import OfficeMap from './OfficeMap'; 
import SeatInfoModal from './SeatInfoModal'; 


const OverviewPage = observer(() => {
  const { user } = useAuthContext() ?? {};

  const userName = user?.claims?.find(claim => claim.key === 'name')?.value;
  const welcomeTitle = `Velkommen ${userName || ''}`; // Handle undefined userName
  const subTitle = 'Vennligst velg rom for å booke';

  const [showModal, setShowModal] = useState(false);
  const [selectedSeatId, setSelectedSeatId] = useState(null);
  const [activeBookings, setActiveBookings] = useState([
    new Booking(1, '1', 'mbs@itverket.no', '2023-01-15'),
    new Booking(2, '2', 'user2@example.com', '2023-01-16'),
    new Booking(3, '3', 'user3@example.com', '2023-01-17'),
    new Booking(4, '4', 'user4@example.com', '2023-01-18'),
    new Booking(5, '5', 'user5@example.com', '2023-01-19')
  ])


  const showSeatInfo = (seatId) => {
    setSelectedSeatId(seatId);
    setShowModal(true);
  };
  

  return (
    <>
      <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 outline-none focus:outline-none">
        <div className="flex flex-col gap-10">
          <Heading title={welcomeTitle} subTitle={subTitle} />
          <div className="flex flex-row gap-24">
            <OfficeMap showSeatInfo={showSeatInfo} activeBookings={activeBookings} user={user}/>
          </div>
        </div>
      </div>
      {showModal && <SeatInfoModal onClose={() => setShowModal(false)} selectedSeatId={selectedSeatId} user={user} activeBookings={activeBookings}/>}
    </>
  );
});

export default OverviewPage;
