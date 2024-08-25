import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-stat-cards',
  standalone: true,
  imports: [],
  templateUrl: './stat-cards.component.html',
  styleUrl: './stat-cards.component.scss'
})
export class StatCardsComponent {
@Input() activeCount:number=0;
@Input() completedCount:number=0;
activePercentage: number = 0;
completedPercentage: number = 0;

ngOnChanges() {
  this.calculatePercentages();
}

calculatePercentages() {
  const total = this.activeCount + this.completedCount;
  if (total > 0) {
    this.activePercentage = Math.floor((this.activeCount / total) * 100);
    this.completedPercentage = Math.floor((this.completedCount / total) * 100);
    
  } else {
    this.activePercentage = 0;
    this.completedPercentage = 0;
  }
}

}
