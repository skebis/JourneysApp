import { AfterViewInit, Component, Inject, Injectable, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
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
export class JourneyListComponent implements OnInit, OnDestroy, AfterViewInit {
  // journeys is the list of all current journeys in a table. Used for showing journeys to user.
  journeys = [];

  dataSource = new MatTableDataSource<Journey>(this.journeys);

  // Mat table definitions
  displayedColumns: string[] = ['name', 'description', 'actions'];

  @ViewChild(MatPaginator)
    paginator!: MatPaginator;

  constructor(private journeyService: JourneyService, public dialog: MatDialog) {

  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
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
    this.journeyService.getJourneys(this.paginator.pageIndex, this.paginator.pageSize).subscribe(res => {
      this.dataSource = new MatTableDataSource(res);
    });
  }

  showJourney(id: string) {
    this.journeyService.getJourney(id).subscribe(res => {
      this.journeys = res;
    });
  }
}
