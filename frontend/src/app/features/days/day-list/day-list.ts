import { Component, inject } from '@angular/core';
import { TripStore } from '../../../core/stores/trip-store';

@Component({
  selector: 'app-day-list',
  imports: [],
  templateUrl: './day-list.html',
  styleUrl: './day-list.css',
})
export class DayList {
  store = inject(TripStore)
}
