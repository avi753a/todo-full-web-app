import { Component, Input, OnInit, effect } from '@angular/core';
import { TaskItemComponent } from '../shared/task-item/task-item.component';
import { TopNavComponent } from '../shared/top-nav/top-nav.component';
import { ContentService } from '../../Services/content.service';
import { NavService } from '../../Services/nav.service';
import { CommonModule } from '@angular/common';
import { TaskView } from '../../Models/TaskView';

@Component({
  selector: 'app-active',
  standalone: true,
  imports: [TaskItemComponent, TopNavComponent, CommonModule],
  templateUrl: './active.component.html',
  styleUrl: './active.component.scss'
})
export class ActiveComponent implements OnInit {
  activeList: TaskView[] = [];
  selectedDate: Date = new Date();
  baseActiveList: TaskView[] = [];
  isToday = true;
  dateText = this.selectedDate.toLocaleDateString('en-US', {
    weekday: 'long',
    day: '2-digit',
    month: 'long',
    year: 'numeric',
  });
  constructor(private contentService: ContentService, private navService: NavService) {
    this.contentService.dataUpdatedSubject.subscribe({
      next: (val) => {
        console.log("Updating data");
        this.loadData();
      }
    })
  }
  ngOnInit(): void {
    this.navService.activeRouteSignal.set("Active");
    this.loadData();
  }
  loadData() {
    this.contentService.getTasksByDate(this.selectedDate).subscribe({
      next: (value: TaskView[]) => {
        this.baseActiveList = value.filter(task => task.status.toLowerCase() !== 'completed');
        this.activeList = this.contentService.DateFilter(this.baseActiveList, this.selectedDate);
      }
    })
  }
  applyDateFilter(event: any) {
    this.selectedDate = new Date(event.target.value as Date);
    this.dateText = this.selectedDate.toLocaleDateString('en-US', {
      weekday: 'long',
      day: '2-digit',
      month: 'long',
      year: 'numeric',
    });
    this.loadData();
    this.isToday = this.selectedDate.getDate().toLocaleString() === new Date().getDate().toLocaleString();

  }
}
