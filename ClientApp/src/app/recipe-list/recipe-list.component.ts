import { Component, Inject, Injectable, OnDestroy, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Recipe } from "../classes/recipe";
import { RecipeDetailsComponent } from "../recipe-details/recipe-details.component";
import { JourneyService } from "../journey-service";

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.css'],
  providers: [JourneyService]
})

@Injectable()
export class RecipeListComponent implements OnInit, OnDestroy {
  // recipes is the list of all current recipes in a table. Used for showing recipes to user.
  recipes: Recipe[] = [];

  // Mat table definitions
  displayedColumns: string[] = ['name', 'description', 'ingredients', 'actions'];

  constructor(private journeyService: JourneyService, public dialog: MatDialog) {

  }

  ngOnInit(): void {
    
  }

  ngOnDestroy() {
    // Unsubscribes here
  }

  ngOnChanges() {
    // Change detection things here
  }

  openRecipeDetailsDialog(recipe: Recipe): void {
    const dialogRef = this.dialog.open(RecipeDetailsComponent ,{
      data: recipe
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('dialog closed');
    });
  }

  showRecipes() {
    this.journeyService.getRecipes().subscribe(res => {
      this.recipes = res;
    });
  }
}
