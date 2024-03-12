import React from "react";

interface ToggleProps{
    isToggled: boolean;
    handleToggleChange: () => void;
}

export const ToggleSwitch = ({isToggled, handleToggleChange}: ToggleProps) => {

    return (   
        <label className='autoSaverSwitch relative inline-flex cursor-pointer select-none items-center'>
            
            <input
                type='checkbox'
                name='autoSaver'
                className='sr-only'
                checked={isToggled}
                onChange={handleToggleChange}
              />
            <span
            className={`slider mr-3 flex h-[26px] w-[50px] items-center rounded-full p-1 duration-200 ${
                isToggled ? 'bg-blue-400' : 'bg-[#CCCCCE]'
            }`}>
                <span
                    className={`dot h-[18px] w-[18px] rounded-full bg-white duration-200 ${
                    isToggled ? 'translate-x-6' : ''
                    }`}>
                </span>
            </span>
            <label className="text-xs">Velg alle seter</label>
        </label>
    );
  };
