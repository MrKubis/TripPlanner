import { Component, inject, OnInit } from '@angular/core';
import { TripStore } from '../../../core/stores/trip-store';
import { ActivatedRoute } from '@angular/router';
import { Button } from "../../../shared/button/button";

@Component({
  selector: 'app-link-card',
  imports: [Button],
  templateUrl: './link-card.html',
  styleUrl: './link-card.css',
})
export class LinkCard implements OnInit{

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
