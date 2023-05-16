import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Journey } from "./classes/journey";

@Injectable()
export class JourneyService {
  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    @Inject('BASE_API_URL') private baseApiUrl: string) {

  }

  public getJourneys(): Observable<any> {
    return this.http.get<Journey[]>(this.baseUrl + this.baseApiUrl);
  }

  public postJourney(journey: Journey): Observable<any> {
    return this.http.post<Journey>(this.baseUrl + this.baseApiUrl, journey);
  }
}
