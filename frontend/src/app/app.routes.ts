import { Routes } from '@angular/router';
import { TripList } from './features/trips/trip-list/trip-list';
import { TripCard } from './features/trips/trip-card/trip-card';
import { DayCard } from './features/days/day-card/day-card';
import { LinkCard } from './features/links/link-card/link-card';
import { DestinationCard } from './features/destinations/destination-card/destination-card';

export const routes: Routes = [
    {
        path:"",
        component:TripList
    },
    {
        path:"trip/:id",
        component: TripCard,
        
    },
    {
        path:"trip/:id/links",
        component:LinkCard
    },
    {
        path:"trip/:id/days",
        component:DayCard
    },
    {
        path:"trip/:id/destinations",
        component:DestinationCard
    }
];
