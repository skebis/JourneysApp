import { Component, Inject, Injectable } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Journey } from "../classes/journey";

@Component({
  selector: 'app-journey-details',
  templateUrl: './journey-details.component.html',
  styleUrls: ['./journey-details.component.css']
})

@Injectable()
export class JourneyDetailsComponent {
  journey: Journey;

  constructor(public dialogRef: MatDialogRef<JourneyDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Journey) {
    this.journey = data;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
