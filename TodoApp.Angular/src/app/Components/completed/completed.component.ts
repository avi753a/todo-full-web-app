import { Component, OnInit, effect } from '@angular/core';
import { TopNavComponent } from '../shared/top-nav/top-nav.component';
import { TaskItemComponent } from "../shared/task-item/task-item.component";
import { ContentService } from '../../Services/content.service';
import { NavService } from '../../Services/nav.service';
import { CommonModule } from '@angular/common';
import { TaskView } from '../../Models/TaskView';

@Component({
    selector: 'app-completed',
    standalone: true,
    templateUrl: './completed.component.html',
    styleUrl: './completed.component.scss',
    imports: [TopNavComponent, TaskItemComponent,CommonModule]
})
export class CompletedComponent implements OnInit{
   completedList:TaskView[]=[];
   selectedDate:Date=new Date();
   isToday=true;
   dateText=this.selectedDate.toLocaleDateString('en-US', {
     weekday: 'long',
     day: '2-digit',
     month: 'long',
     year: 'numeric',
   });

   constructor(private contentService:ContentService,private navService:NavService){
    this.contentService.dataUpdatedSubject.subscribe({
      next:(val)=>{
        console.log("Updating data");
        this.loadData();
      }
     })
   }

  ngOnInit(): void {
   this.navService.activeRouteSignal.set("Completed");
 
    this.loadData();
 }

 loadData(){
  console.log("Callled completed date load data");
  this.contentService.getTasksByDate(this.selectedDate).subscribe({
    next:(value:TaskView[])=>{
      this.completedList = value.filter(task => task.status.toLowerCase() === 'completed');
      console.log("Loaded data",this.completedList);
    }
   })
 }
 
 applyDateFilter(event:any){
  this.selectedDate=new Date(event.target.value as Date);
  this.dateText=this.selectedDate.toLocaleDateString('en-US', {
    weekday: 'long',
    day: '2-digit',
    month: 'long',
    year: 'numeric',
  });
this.loadData();
this.isToday=this.selectedDate.getDate().toLocaleString()===new Date().getDate().toLocaleString();

}
}
