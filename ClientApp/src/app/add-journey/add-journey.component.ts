import { Component, Injectable } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Journey } from '../classes/journey';
import { JourneyService } from '../journey-service';

@Component({
  selector: 'app-add-journey',
  templateUrl: './add-journey.component.html',
  styleUrls: ['./add-journey.component.css'],
  providers: [JourneyService]
})

@Injectable()
export class AddJourneyComponent {

  private now: Date = new Date();

  // journey is the current journey that is being edited and added.
  journey: Journey = {
    departure: this.now,
    return: this.now,
    departureStationId: 0,
    departureStationName: '',
    returnStationId: 0,
    returnStationName: '',
    coveredDistance: 0,
    duration: 0
  };

  constructor(private journeyService: JourneyService,
    public dialogRef: MatDialogRef<AddJourneyComponent>) {

  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  // Adds new journey by calling API backend.
  addJourney() {
    this.journeyService.postJourney(this.journey).subscribe(res => {
      console.log(res + " got answer");
    });
    this.clearJourney();
    this.dialogRef.close();
  }

  // Clears current journey, making it empty.
  private clearJourney() {
    this.journey = {
      departure: this.now,
      return: this.now,
      departureStationId: 0,
      departureStationName: '',
      returnStationId: 0,
      returnStationName: '',
      coveredDistance: 0,
      duration: 0
    }
  }

}
