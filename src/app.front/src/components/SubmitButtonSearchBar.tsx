import React from 'react'
import SearchIcon from '../icons/search.svg'

interface SubmitButtonSearchBarProps {
    text: string
    active?: boolean
    onSubmit: () => void
}
function SubmitButtonSearchBar({text,active, onSubmit}: SubmitButtonSearchBarProps) {
  return (
    <button onClick={onSubmit} className={`transition-all rounded-full bg-gradient-to-r from-rose-500 to-fuchsia-700 flex flex-row gap-[10px] justify-center items-center ml-[32px] mr-[10px] ${!active ? 'w-[50px] h-[50px]': 'px-[32px] py-[15px]'}`}>
        <img width={25} src={SearchIcon}/>
        {active && <span className='text-white font-bold'>{text}</span>}
    </button>
  )
}

export default SubmitButtonSearchBar