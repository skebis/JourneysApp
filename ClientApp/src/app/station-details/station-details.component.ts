import { Component, Inject, Injectable } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Station } from "../classes/station";

@Component({
  selector: 'app-station-details',
  templateUrl: './station-details.component.html',
  styleUrls: ['./station-details.component.css']
})

@Injectable()
export class StationDetailsComponent {
  station: Station;

  constructor(public dialogRef: MatDialogRef<StationDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Station) {
    this.station = data;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
