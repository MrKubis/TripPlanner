import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { PaginatedResult, Trip, TripShort } from '../models/trip.models';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TripService {
  private http = inject(HttpClient);
  //hardcoded for now

  private base = environment.apiUrl + '/api/v1/trip'

  getAll() { return this.http.get<PaginatedResult<TripShort>>(this.base); }
  getById(id: string) { return this.http.get<Trip>(`${this.base}/${id}`); }
}
