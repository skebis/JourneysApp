import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AddJourneyComponent } from './add-journey/add-journey.component';
import { AddStationComponent } from './add-station/add-station.component';
import { JourneyListComponent } from './journey-list/journey-list.component';
import { StationListComponent } from './station-list/station-list.component';
import { JourneyDetailsComponent } from './journey-details/journey-details.component';
import { StationDetailsComponent } from './station-details/station-details.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AddJourneyComponent,
    AddStationComponent,
    JourneyListComponent,
    StationListComponent,
    JourneyDetailsComponent,
    StationDetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'journeys', component: JourneyListComponent },
      { path: 'journey/:id', component: JourneyDetailsComponent },
      { path: 'stations', component: StationListComponent },
      { path: 'station/:id', component: StationDetailsComponent },
    ]),
    MaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
