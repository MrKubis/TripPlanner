import { inject, Injectable, signal } from '@angular/core';
import { Trip, TripShort } from '../models/trip.models';
import { TripService } from '../services/trip-service';
import { DayService } from '../services/day-service';
import { DestinationService } from '../services/destination-service';

@Injectable({
  providedIn: 'root',
})
export class TripStore {


  private tripService = inject(TripService);
  private dayService = inject(DayService);
  private destinationService = inject(DestinationService);

  trips = signal<TripShort[]>([]);
  selectedTrip = signal<Trip | null>(null);
  loading = signal(false);
  error = signal<string | null>(null);

  loadTrips(){
    this.loading.set(true);
    this.tripService.getAll().subscribe({
      next: response => {
        console.log("S")
        this.trips.set(response.data); this.loading.set(false);
      },
      error: err=> {this.error.set(err); this.loading.set(false);}
    })
  }
  loadTrip(id: string){
    this.loading.set(true);
    this.tripService.getById(id).subscribe({
      next: trip => {
        this.selectedTrip.set(trip);
        this.loading.set(false); },
      error: err => { 
        this.error.set(err.message); 
        this.loading.set(false); }
    })
  }
}
