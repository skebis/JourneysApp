import { Component, Inject, Injectable } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Recipe } from "../classes/recipe";

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrls: ['./recipe-details.component.css']
})

@Injectable()
export class RecipeDetailsComponent {
  recipe: Recipe;

  constructor(public dialogRef: MatDialogRef<RecipeDetailsComponent>,
   @Inject(MAT_DIALOG_DATA) public data: Recipe) {
    this.recipe = data;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
