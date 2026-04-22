import { Routes } from '@angular/router';
import { TripList } from './features/trips/trip-list/trip-list';
import { TripCard } from './features/trips/trip-card/trip-card';

export const routes: Routes = [
    {
        path:"",
        component:TripList
    },
    {
        path:"trip/:id",
        component: TripCard
    }

];
