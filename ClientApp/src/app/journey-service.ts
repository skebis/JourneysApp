import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Recipe } from "./classes/recipe";

@Injectable()
export class JourneyService {
  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    @Inject('BASE_API_URL') private baseApiUrl: string) {

  }

  public getRecipes(): Observable<any> {
    return this.http.get<Recipe[]>(this.baseUrl + this.baseApiUrl);
  }

  public postRecipe(recipe: Recipe): Observable<any> {
    return this.http.post<Recipe>(this.baseUrl + this.baseApiUrl, recipe);
  }
}
