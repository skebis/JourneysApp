import { AfterViewInit, Component, Inject, Injectable, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
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
  journeys: any[] = [];
  loading: boolean = true;

  dataSource = new MatTableDataSource<any>();

  // Mat table definitions
  displayedColumns: string[] = ['departureStation', 'returnStation', 'distance', 'duration', 'actions'];

  @ViewChild(MatPaginator)
    paginator!: MatPaginator;

  constructor(private journeyService: JourneyService, public dialog: MatDialog) {

  }

  ngAfterViewInit() {
    this.showJourneys();
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

    dialogRef.afterClosed().subscribe(() => {
      console.log('dialog closed');
    });
  }

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

  pageChanged(event: any) {
    this.loading = true;
    let pageIndex = event.pageIndex;
    let pageSize = event.pageSize;

    let previousIndex = event.previousPageIndex;

    let previousSize = pageSize * pageIndex;

    this.showNextJourneys(previousSize, pageIndex, pageSize);
  }

  /*showJourney(id: string) {
    this.journeyService.getJourney(id).subscribe(res => {
      this.journeys = res;
    });
  }*/
}
