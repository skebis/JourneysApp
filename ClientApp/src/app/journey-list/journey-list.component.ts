import { AfterViewInit, Component, Injectable, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { Router } from "@angular/router";
import { Journey } from "../classes/journey";
import { JourneyService } from "../journey-service";

@Component({
  selector: 'app-journey-list',
  templateUrl: './journey-list.component.html',
  styleUrls: ['./journey-list.component.css'],
  providers: [JourneyService]
})

@Injectable()
export class JourneyListComponent implements AfterViewInit {
  journeys: any[] = [];
  loading: boolean = true;

  // Mat table data.
  dataSource = new MatTableDataSource<any>();

  // Mat table definitions
  displayedColumns: string[] = ['departureStation', 'returnStation', 'distance', 'duration', 'actions'];

  @ViewChild(MatPaginator)
    paginator!: MatPaginator;

  constructor(private journeyService: JourneyService, public dialog: MatDialog, private router: Router) {
  }

  ngAfterViewInit() {
    this.showJourneys();
  }

  // Open single journey details.
  openJourneyDetails(journey: Journey): void {
    this.router.navigateByUrl('/journey/' + journey.journeyId);
  }

  // Initial request to show journeys.
  showJourneys() {
    this.journeyService.getJourneys(this.paginator.pageIndex, this.paginator.pageSize)
      .subscribe(res => {
        this.loading = false;
        this.journeys = res.data;

        this.journeys.length = res.dataCount;

        this.dataSource = new MatTableDataSource<any>(this.journeys);
        this.dataSource.paginator = this.paginator;
    });
  }

  // Show journeys, triggered by paginator events.
  showNextJourneys(currentSize: number, page: number, pageSize: number) {
    this.journeyService.getJourneys(page, pageSize)
      .subscribe(res => {
        this.loading = false;
        this.journeys.length = currentSize;
        this.journeys.push(...res.data);

        this.journeys.length = res.dataCount;

        this.dataSource = new MatTableDataSource<any>(this.journeys);
        this.dataSource._updateChangeSubscription();
        this.dataSource.paginator = this.paginator;
      });
  }

  // Triggered when paginator pages and item count are changed.
  pageChanged(event: any) {
    this.loading = true;
    let pageIndex = event.pageIndex;
    let pageSize = event.pageSize;

    let previousSize = pageSize * pageIndex;

    this.showNextJourneys(previousSize, pageIndex, pageSize);
  }

}
