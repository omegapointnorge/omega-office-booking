import Heading from "../../components/Heading";
import { useAuthContext } from "../../api/useAuthContext";
import { observer } from "mobx-react-lite";
import { useNavigate } from "react-router-dom";
import { useStores } from "../../stores";

const OverviewPage = observer(() => {
  const context = useAuthContext();
  const navigate = useNavigate();
  const name = context?.user?.claims?.find((x) => x.key === "name")?.value;
  const { overviewStore } = useStores();

  return (
    <>
      <div
        className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none"
      >
        <div className="flex flex-col gap-10">
          <Heading
            title={`Velkommen ${name}`}
            subTitle="Vennligst velg rom for å booke"
          />
          <div className="flex flex-row gap-24">
            {overviewStore.rooms.map((room) => {
              return (
                <div key={room.id}>
                  <p className="font-bold text-center">{room.name}</p>
                  <img
                    alt={room.id}
                    className="hidden md:block cursor-pointer"
                    src={`/images/${room.image}`}
                    onClick={() => {
                      navigate(
                        `/rooms/${overviewStore.getRouteName(room.name)}`
                      );
                    }}
                  />
                </div>
              );
            })}
          </div>
        </div>
      </div>
    </>
  );
});

export default OverviewPage;
