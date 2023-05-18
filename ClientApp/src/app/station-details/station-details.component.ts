import { Component, Injectable } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Subscription } from "rxjs";
import { JourneyService } from "../journey-service";

@Component({
  selector: 'app-station-details',
  templateUrl: './station-details.component.html',
  styleUrls: ['./station-details.component.css'],
  providers: [JourneyService]
})

@Injectable()
export class StationDetailsComponent {
  station: any;
  id: any;
  loading: boolean = true;
  subscription: Subscription[] = [];

  constructor(private journeyService: JourneyService, private route: ActivatedRoute) {
    
  }

  ngOnInit() {
    this.subscription.push(this.route.paramMap.subscribe(param => {
      this.id = param.get('id');
      this.journeyService.getStation(this.id).subscribe(res => {
        this.loading = false;
        this.station = res.station;
        this.station.departureCount = res.departureStationCount;
        this.station.returnCount = res.returnStationCount;
      })
    }))
  }

  ngOnDestroy() {
    this.subscription.forEach(sub => sub.unsubscribe());
  }
}
