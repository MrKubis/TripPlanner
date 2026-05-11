import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TripStore } from '../../../core/stores/trip-store';

@Component({
  selector: 'app-destination-card',
  imports: [],
  templateUrl: './destination-card.html',
  styleUrl: './destination-card.css',
})
export class DestinationCard implements OnInit {
  private route = inject(ActivatedRoute)
  store=inject(TripStore)
  ngOnInit(): void {
    this.route.params.subscribe(params =>{
      const id = params['id'];
      if(id){
        this.store.loadTrip(id);
      }
    })
  }
}
