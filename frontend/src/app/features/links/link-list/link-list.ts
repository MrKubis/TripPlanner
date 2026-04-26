import { Component, inject } from '@angular/core';
import { TripStore } from '../../../core/stores/trip-store';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-link-list',
  imports: [RouterLink],
  templateUrl: './link-list.html',
  styleUrl: './link-list.css',
})
export class LinkList {
  store=inject(TripStore)
}
