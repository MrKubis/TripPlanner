import { Component, inject, OnInit } from '@angular/core';
import { TripStore } from '../../../core/stores/trip-store';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-destination-list',
  imports: [RouterLink],
  templateUrl: './destination-list.html',
  styleUrl: './destination-list.css',
})
export class DestinationList{
  store = inject(TripStore)
}
