import { useState, useEffect} from 'react'
import './App.css'

function App() {
    /* eslint-disable  @typescript-eslint/no-explicit-any */
    const [cars, setCar] = useState<any[]>([]);

    useEffect(() => {
        fetch("http://localhost:5000/api/Car/GetAllCar")
            .then((response) => response.json())
            .then((data) => {
                setCar(data);
            })
            .catch((error) => console.log(error));
    }, []);


    return (
        <div>
            <h2>Cars in DB:</h2>
            {cars?.map(d=> (
                <p key={d.id}>{d.model}</p>
            ))} 
        </div>
    );
}

export default App
