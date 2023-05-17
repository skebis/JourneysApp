import { HttpClient, HttpParams } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Journey } from "./classes/journey";
import { Station } from "./classes/station";

@Injectable()
export class JourneyService {
  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    @Inject('BASE_JOURNEYS_API_URL') private baseJourneysApiUrl: string,
    @Inject('BASE_STATIONS_API_URL') private baseStationsApiUrl: string) {

  }

  public getJourneys(pageIndex: number, pageSize: number): Observable<any> {
    let queryParams = new HttpParams
    queryParams = queryParams.append("Page", pageIndex);
    queryParams = queryParams.append("PageSize", pageSize);
    return this.http.get<Journey[]>(this.baseUrl + this.baseJourneysApiUrl, { params: queryParams });
  }

  public postJourney(journey: Journey): Observable<any> {
    return this.http.post<Journey>(this.baseUrl + this.baseJourneysApiUrl, journey);
  }

  public getJourney(id: string): Observable<any> {
    return this.http.get<Journey>(this.baseUrl + this.baseJourneysApiUrl + '/' + id);
  }

  public getStations(): Observable<any> {
    return this.http.get<Station[]>(this.baseUrl + this.baseStationsApiUrl);
  }

  public getStation(id: string): Observable<any> {
    return this.http.get<Station>(this.baseUrl + this.baseStationsApiUrl + '/' + id);
  }

  public postStation(station: Station): Observable<any> {
    return this.http.post<Station>(this.baseUrl + this.baseStationsApiUrl, station);
  }
}
