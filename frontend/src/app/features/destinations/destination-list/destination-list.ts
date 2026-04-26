import { Component, inject, OnInit } from '@angular/core';
import { TripStore } from '../../../core/stores/trip-store';

@Component({
  selector: 'app-destination-list',
  imports: [],
  templateUrl: './destination-list.html',
  styleUrl: './destination-list.css',
})
export class DestinationList{
  store = inject(TripStore)
}
