import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Trip } from '../../../core/models/trip.models';
import { TripStore } from '../../../core/stores/trip-store';
import { DestinationList } from "../../destinations/destination-list/destination-list";
import { DayList } from "../../days/day-list/day-list";
import { LinkList } from '../../links/link-list/link-list';

@Component({
  selector: 'app-trip-card',
  imports: [DestinationList, DayList, LinkList],
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
