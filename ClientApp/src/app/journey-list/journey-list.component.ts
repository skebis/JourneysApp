import { Component, Inject, Injectable, OnDestroy, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Journey } from "../classes/journey";
import { JourneyDetailsComponent } from "../journey-details/journey-details.component";
import { JourneyService } from "../journey-service";

@Component({
  selector: 'app-journey-list',
  templateUrl: './journey-list.component.html',
  styleUrls: ['./journey-list.component.css'],
  providers: [JourneyService]
})

@Injectable()
export class JourneyListComponent implements OnInit, OnDestroy {
  // journeys is the list of all current journeys in a table. Used for showing journeys to user.
  journeys: Journey[] = [];

  // Mat table definitions
  displayedColumns: string[] = ['name', 'description', 'actions'];

  constructor(private journeyService: JourneyService, public dialog: MatDialog) {

  }

  ngOnInit(): void {
    
  }

  ngOnDestroy() {
    // Unsubscribes here
  }

  ngOnChanges() {
    // Change detection things here
  }

  openJourneyDetailsDialog(journey: Journey): void {
    const dialogRef = this.dialog.open(JourneyDetailsComponent ,{
      data: journey
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('dialog closed');
    });
  }

  showJourneys() {
    this.journeyService.getJourneys().subscribe(res => {
      this.journeys = res;
    });
  }
}
