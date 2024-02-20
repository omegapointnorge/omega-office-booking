import React, { useState, useEffect } from "react";
import { observer } from "mobx-react-lite";
import bookingStore from "../../../state/stores/BookingStore";
import { useAuthContext } from "../../../core/auth/useAuthContext";
import { Booking } from "@/shared/types/entities";
import { isSameDate } from "@/shared/utils";
import toast from "react-hot-toast";
import { SeatInfoComponent } from "./SeatInfoComponent";

interface OverviewSeatInfoProps {
  onClose: () => void;
  selectedSeatId: number;
}

const RECAPTCHA_SITE_KEY = "6Lc1tV8pAAAAABKV5g3LrYZNzUx1KGQkYHR-hSzo";

const OverviewSeatInfo = observer(
  ({ onClose, selectedSeatId }: OverviewSeatInfoProps) => {
    const { user } = useAuthContext() ?? {};
    const isEventAdmin = user.claims.role === "EventAdmin";
    const userGuid = user.claims.objectidentifier;

    const { activeBookings, displayDate } = bookingStore;
    const [selectedBooking, setSelectedBooking] = useState<Booking>();
    const [loading, setLoading] = useState(false);

    useEffect(() => {
      const foundBooking = activeBookings.find(
        (booking) =>
          booking.seatId === selectedSeatId &&
          isSameDate(displayDate, booking.bookingDateTime)
      );
      if (foundBooking) {
        setSelectedBooking(foundBooking);
      }
    }, [selectedSeatId, activeBookings, displayDate]);

    useEffect(() => {
      // Dynamically load reCAPTCHA script
      const script = document.createElement("script");
      script.src = `https://www.google.com/recaptcha/enterprise.js?render=${RECAPTCHA_SITE_KEY}`;
      script.async = true;
      document.head.appendChild(script);

      // Clean up function to remove the script when the component is unmounted
      return () => {
        document.head.removeChild(script);
      };
    }, []);

    const executeRecaptcha = async (): Promise<string> => {
      // Execute reCAPTCHA and return the token
      return new Promise((resolve) => {
        (window as any).grecaptcha.enterprise.ready(async () => {
          const token = await (window as any).grecaptcha.enterprise.execute(
            RECAPTCHA_SITE_KEY,
            { action: "BOOKING" }
          );
          resolve(token);
        });
      });
    };

    const handleBooking = async () => {
      setLoading(true);
      const reCAPTCHAtoken = await executeRecaptcha();
      await bookingStore
        .createBookingRequest(selectedSeatId, reCAPTCHAtoken)
        .then(() => setLoading(false));
      onClose();
    };

    const handleDelete = async () => {
      setLoading(true);
      if (userGuid !== selectedBooking?.userId) {
        toast.success(
          `Vennligst informer ${selectedBooking?.userName} om at du har kansellert reservasjonen deres?`
        );
      }
      if (selectedBooking?.id) {
        await bookingStore.deleteBooking(selectedBooking.id).then(() => {
          setLoading(false);
          onClose();
        });
      } else {
        console.error("Unable to delete on id");
      }
    };

    return (
      <SeatInfoComponent
        loading={loading}
        displayDate={displayDate}
        selectedSeatId={selectedSeatId}
        selectedBooking={selectedBooking}
        onClose={onClose}
        userGuid={userGuid}
        isEventAdmin={isEventAdmin}
        handleBooking={handleBooking}
        handleDelete={handleDelete}
      />
    );
  }
);

export default OverviewSeatInfo;
