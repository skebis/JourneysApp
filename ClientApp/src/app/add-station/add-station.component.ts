import { Component, Injectable} from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Station } from '../classes/station';
import { JourneyService } from '../journey-service';

@Component({
  selector: 'app-add-station',
  templateUrl: './add-station.component.html',
  styleUrls: ['./add-station.component.css'],
  providers: [JourneyService]
})

@Injectable()
export class AddStationComponent {

  // station is the current station that is being edited and added.
  station: Station = {
    idInt: 0,
    nameFinnish: "",
    nameSwedish: "",
    nameEnglish: "",
    addressFinnish: "",
    addressSwedish: "",
    cityFinnish: "",
    citySwedish: "",
    operator: "",
    capacity: 0,
    locationX: 0,
    locationY: 0
  };

  constructor(private journeyService: JourneyService,
    public dialogRef: MatDialogRef<AddStationComponent>) {

  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  // Adds new station by calling API backend.
  addStation() {
    this.journeyService.postStation(this.station).subscribe(res => {
      console.log(res + " got answer");
    });
    this.clearStation();
    this.dialogRef.close();
  }

  // Clears current station, making it empty.
  private clearStation() {
    this.station = {
      idInt: 0,
      nameFinnish: "",
      nameSwedish: "",
      nameEnglish: "",
      addressFinnish: "",
      addressSwedish: "",
      cityFinnish: "",
      citySwedish: "",
      operator: "",
      capacity: 0,
      locationX: 0,
      locationY: 0
    }
  }

}
