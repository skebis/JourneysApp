import { AfterViewInit, Component, Injectable, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { Router } from "@angular/router";
import { Station } from "../classes/station";
import { JourneyService } from "../journey-service";

@Component({
  selector: 'app-station-list',
  templateUrl: './station-list.component.html',
  styleUrls: ['./station-list.component.css'],
  providers: [JourneyService]
})

@Injectable()
export class StationListComponent implements AfterViewInit {
  stations: any[] = [];
  loading: boolean = true;

  // Mat table data
  dataSource = new MatTableDataSource<any>();

  // Mat table definitions
  displayedColumns: string[] = ['name', 'address', 'actions'];

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  constructor(private journeyService: JourneyService, private router: Router) {

  }

  ngAfterViewInit() {
    this.showStations();
  }

  // Open single station details.
  openStationDetails(station: Station): void {
    this.router.navigateByUrl('/station/' + station.stationId);
  }

  // Show all stations in a table.
  showStations() {
    this.journeyService.getStations()
      .subscribe(res => {
        this.loading = false;
        this.stations = res;
        this.dataSource = new MatTableDataSource<any>(this.stations);
        this.dataSource.paginator = this.paginator;
      });
  }

  // Triggered when paginator pages and item count are changed.
  pageChanged(event: any) {
    this.loading = true;
    this.showStations();
  }
}
