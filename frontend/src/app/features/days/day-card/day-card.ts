import { Component, inject, OnInit } from '@angular/core';
import { TripStore } from '../../../core/stores/trip-store';
import { ActivatedRoute } from '@angular/router';
import { Button } from "../../../shared/button/button";

@Component({
  selector: 'app-day-card',
  imports: [Button],
  templateUrl: './day-card.html',
  styleUrl: './day-card.css',
})
export class DayCard implements OnInit{
  store = inject(TripStore);
  private route = inject(ActivatedRoute);

ngOnInit(): void {
    const tripId = this.route.snapshot.params['id'];
    const dayId = this.route.snapshot.params['dayId'];
    
    console.log(tripId, dayId)
    
    if (!this.store.selectedTrip()) {
      this.store.loadTrip(tripId);
    }
  }

}
