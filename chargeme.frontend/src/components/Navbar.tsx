import { useState, useEffect } from 'react'

import {
    Avatar,
    AvatarFallback,
    AvatarImage,
} from "@/components/ui/avatar"

interface NavbarProps {
    name: string,
    surname: string
}

export const Navbar: React.FC<NavbarProps> = ({ name, surname }) => {
    const [initial, setInitial] = useState<string>('')

    useEffect(() => {
        const firstLetterName = name ? name.charAt(0).toUpperCase() : ''
        const firstLetterSurname = surname ? surname.charAt(0).toUpperCase() : ''

        setInitial(firstLetterName + firstLetterSurname)
    }, [])


    return (
        <>
            <nav className='fixed bottom-0 h-24 w-full bg-white'>
                <ul className=' relative flex justify-around w-100 h-100' style={{ top: '50%', transform: 'translateY(-50%)', textAlign: 'center' }}>
                    <li>
                        mappa
                    </li>
                    <li>
                        home
                    </li>
                    <li>
                        <Avatar>
                            <AvatarImage  alt="@shadcn" /> 
                            <AvatarFallback>{initial}</AvatarFallback>
                        </Avatar>
                    </li>
                </ul>
            </nav>
        </>
    )
}