import { ChangeDetectorRef, Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Recipe } from '../classes/recipe';
import { JourneyService } from '../journey-service';

@Component({
  selector: 'app-add-journey',
  templateUrl: './add-journey.component.html',
  styleUrls: ['./add-journey.component.css'],
  providers: [JourneyService]
})

@Injectable()
export class AddJourneyComponent implements OnInit, OnDestroy {

  // recipe is the current recipe that is being edited and added.
  recipe: Recipe = {
    name: '',
    ingredients: [],
    description: ''
  }

  constructor(private journeyService: JourneyService,
    public dialogRef: MatDialogRef<AddJourneyComponent>) {

  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {

  }

  ngOnDestroy() {
    // Unsubscribes and such
  }

  ngOnChanges() {
    // Change detection things here
  }

  // Adds new recipe by calling API backend.
  addRecipe() {
    this.journeyService.postRecipe(this.recipe).subscribe(res => {
      console.log(res + " got answer");
    });
    this.clearRecipe();
    this.dialogRef.close();
  }

  // Adds new empty ingredient to current recipe.
  addNewIngredient() {
    this.recipe.ingredients.push({
        name: '',
        amount: 0,
        unit: ''
    });
  }

  // Clears current recipe, making it empty.
  private clearRecipe() {
    this.recipe = {
      name: '',
      ingredients: [],
      description: ''
    }
  }

}
