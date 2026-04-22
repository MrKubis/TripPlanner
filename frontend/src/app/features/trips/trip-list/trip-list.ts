import { Component, inject, OnInit } from '@angular/core';
import { TripStore } from '../../../core/stores/trip-store';
import { TripShort } from '../../../core/models/trip.models';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-trip-list',
  imports: [RouterLink],
  templateUrl: './trip-list.html',
  styleUrl: './trip-list.css',
})
export class TripList implements OnInit{
  store = inject(TripStore)
  ngOnInit(): void {
    this.store.loadTrips()
  }
  
}
