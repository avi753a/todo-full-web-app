import { Component ,Input, OnInit} from '@angular/core';
import { ContentService } from '../../../Services/content.service';
import { ActivatedRoute, Router, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../../Services/toast.service';
import { TaskView } from '../../../Models/TaskView';
import { TaskDetails } from '../../../Models/TaskDetails';
import { TaskRegistration } from '../../../Models/TaskRegistration';

@Component({
  selector: 'app-task-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './task-item.component.html',
  styleUrl: './task-item.component.scss'
})
export class TaskItemComponent implements OnInit{
  @Input() taskDetails!:TaskView;
  isCompleted!:boolean;
  daysDifference:number=0;
  hourDifference:number=0;
  


  ngOnInit(): void {
    this.isCompleted = this.taskDetails.status.toLowerCase() === "completed";
    this.taskDetails.createdTime = new Date(this.taskDetails.createdTime);
    var differenceInMillis = (new Date()).getTime() - (this.taskDetails.createdTime as Date).getTime();
    this.daysDifference = Math.floor(differenceInMillis / (1000 * 60 * 60 * 24));
    this.hourDifference = Math.floor((differenceInMillis % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
  
  }
  
  constructor(private contentService:ContentService,private activatedRoute:ActivatedRoute,private router:Router,private toastService:ToastService){
    if(this.taskDetails){
    this.getDateTimeDifference(this.taskDetails.editedTime,new Date());
    }
    }
  isInfoView:boolean=false;
  toggleDetails(){
    this.isInfoView=!this.isInfoView;
  }
  deleteItem(id:string){
    this.contentService.deleteTask(id).subscribe({
      next:()=>{
        this.router.navigate([this.activatedRoute.snapshot.url[0].path]);
        this.contentService.dataUpdatedSubject.next(1);
        this.toastService.showSecondary("Task Deleted");
      
      }
    })
  }
  editTask(id:string){
        this.contentService.editTaskSubject.next(id);
  }
  updateStatus() {
    this.contentService.getTaskDetails(this.taskDetails.id).subscribe({
        next: (taskIdDetails: TaskDetails) => {
            taskIdDetails.statusId = taskIdDetails.statusId === 1 ? 2 : 1;
            var dataToUpdate: TaskRegistration = new TaskRegistration(
                taskIdDetails.name,
                taskIdDetails.description,
              taskIdDetails.priorityId,
              taskIdDetails.statusId
            );
            this.contentService.editTask(dataToUpdate, this.taskDetails.id).subscribe({
                next: (value) => {
                    this.contentService.dataUpdatedSubject.next(1);
                    this.toastService.showInfo("Status Changed");
                }
            });
        },
      
    });
}
   getDateTimeDifference(date1: Date, date2: Date){
    console.log("rimedifference");
    // Ensure date1 is not before date2
    if (date1 < date2) {
      const temp = date1;
      date1 = date2;
      date2 = temp;
    }
  
    const differenceInMilliseconds = date1.getTime() - date2.getTime();
    const oneDayInMilliseconds = 1000 * 60 * 60 * 24;
    const oneHourInMilliseconds = 1000 * 60 * 60;
  
    const days = Math.floor(differenceInMilliseconds / oneDayInMilliseconds);
    const remainingMilliseconds = differenceInMilliseconds % oneDayInMilliseconds;
    const hours = Math.floor(remainingMilliseconds / oneHourInMilliseconds);
      this.daysDifference=days;
      this.hourDifference=hours;
  }
}
