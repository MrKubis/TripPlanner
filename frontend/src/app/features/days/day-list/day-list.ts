import { Component, inject } from '@angular/core';
import { TripStore } from '../../../core/stores/trip-store';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-day-list',
  imports: [RouterLink],
  templateUrl: './day-list.html',
  styleUrl: './day-list.css',
})
export class DayList {
  store = inject(TripStore)
}
