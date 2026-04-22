import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Trip } from '../../../core/models/trip.models';
import { TripStore } from '../../../core/stores/trip-store';

@Component({
  selector: 'app-trip-card',
  imports: [],
  templateUrl: './trip-card.html',
  styleUrl: './trip-card.css',
})
export class TripCard  implements OnInit{
  store = inject(TripStore)
  private route = inject(ActivatedRoute)

  ngOnInit(): void {
    this.route.params.subscribe(params =>{
      const id = params['id'];
      if(id){
        this.store.loadTrip(id);
      }
    })
  }
}
