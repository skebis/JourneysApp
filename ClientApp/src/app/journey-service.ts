import { HttpClient, HttpParams } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Journey } from "./classes/journey";

@Injectable()
export class JourneyService {
  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    @Inject('BASE_API_URL') private baseApiUrl: string) {

  }

  public getJourneys(pageIndex: number, pageSize: number): Observable<any> {
    let queryParams = new HttpParams
    queryParams = queryParams.append("Page", pageIndex);
    queryParams = queryParams.append("PageSize", pageSize);
    return this.http.get<Journey[]>(this.baseUrl + this.baseApiUrl, { params: queryParams });
  }

  public postJourney(journey: Journey): Observable<any> {
    return this.http.post<Journey>(this.baseUrl + this.baseApiUrl, journey);
  }

  public getJourney(id: string): Observable<any> {
    return this.http.get<Journey[]>(this.baseUrl + this.baseApiUrl + '/' + id);
  }
}
