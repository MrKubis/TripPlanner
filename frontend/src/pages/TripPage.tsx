import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { Trip } from "../models/Trip";
import api from "../api/api";

export default function TripPage(){
    const params = useParams();
    const tripId = params.tripId;
    console.log(tripId);


    const [error,setError]= useState<Error | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [trip,setTrip] = useState<Trip | null>(null);
    
    function fetchTrip(id: string | undefined) {
        if (tripId == undefined){
        setError(new Error("Trip id undefined"))
        return;
    }
        const response = api.get(`trip/${id}`)
        .finally(()=>
        {
            
        })
        console.log(response);
    }
    
    useEffect(()=>{
        setIsLoading(true);
        fetchTrip(tripId);
        setIsLoading(false)
    })


    if(isLoading) return <p>Loading...</p>
    if(error) return <p>Error: {error.message}</p>
    return(
    <>
        <p>This is a trip page!</p>
    </>
    );
}
