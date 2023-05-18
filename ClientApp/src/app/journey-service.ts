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

  // Get certain number of journeys. Creates a http get query where
  // pageIndex and pageSize is given as query parameters for restricting the number of journeys.
  public getJourneys(pageIndex: number, pageSize: number): Observable<any> {
    let queryParams = new HttpParams
    queryParams = queryParams.append("Page", pageIndex);
    queryParams = queryParams.append("PageSize", pageSize);
    return this.http.get<Journey[]>(this.baseUrl + this.baseJourneysApiUrl, { params: queryParams });
  }

  // Post a single journey object.
  public postJourney(journey: Journey): Observable<any> {
    return this.http.post<Journey>(this.baseUrl + this.baseJourneysApiUrl, journey);
  }

  // Get a single journey object.
  public getJourney(id: string): Observable<any> {
    return this.http.get<Journey>(this.baseUrl + this.baseJourneysApiUrl + id);
  }

  // Get all stations.
  public getStations(): Observable<any> {
    return this.http.get<Station[]>(this.baseUrl + this.baseStationsApiUrl);
  }

  // Get a single station object. Also returns departure count and return count.
  public getStation(id: string): Observable<any> {
    return this.http.get<Station>(this.baseUrl + this.baseStationsApiUrl + id);
  }

  // Post a single station object.
  public postStation(station: Station): Observable<any> {
    return this.http.post<Station>(this.baseUrl + this.baseStationsApiUrl, station);
  }
}
