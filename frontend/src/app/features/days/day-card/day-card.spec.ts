import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DayCard } from './day-card';

describe('DayCard', () => {
  let component: DayCard;
  let fixture: ComponentFixture<DayCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DayCard],
    }).compileComponents();

    fixture = TestBed.createComponent(DayCard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
