import { AfterViewInit, Component, Inject, Injectable, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { Subscription } from "rxjs";
import { JourneyService } from "../journey-service";

@Component({
  selector: 'app-journey-details',
  templateUrl: './journey-details.component.html',
  styleUrls: ['./journey-details.component.css'],
  providers: [JourneyService]
})

@Injectable()
export class JourneyDetailsComponent {
  journey: any;
  id: any;
  loading: boolean = true;
  subscription: Subscription[] = [];

  constructor(private journeyService: JourneyService, private router: Router, private route: ActivatedRoute) {
    
  }

  ngOnInit() {
    this.subscription.push(this.route.paramMap.subscribe(param => {
      this.id = param.get('id');
      this.journeyService.getJourney(this.id).subscribe(res => {
        this.loading = false;
        this.journey = res;
      })
    }))
  }

  ngOnDestroy() {
    this.subscription.forEach(sub => sub.unsubscribe());
  }
}
