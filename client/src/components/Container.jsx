const Container = ({ children }) => {
  return (
    <>
      <div
        className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
 fixed inset-0 outline-none focus:outline-none"
      >
        {children}
      </div>
    </>
  );
};

export default Container;
