import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddJourneyComponent } from '../add-journey/add-journey.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  constructor(public dialog: MatDialog) {

  }

  openAddJourneyDialog(): void {
    const dialogRef = this.dialog.open(AddJourneyComponent, {
      
    });

    dialogRef.afterClosed().subscribe(() => {
      console.log('dialog closed');
    });
  }
}
