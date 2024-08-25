import { Component ,OnInit, effect} from '@angular/core';
import { StatCardsComponent } from '../stat-cards/stat-cards.component';
import { TaskItemComponent } from '../shared/task-item/task-item.component';
import { AuthService } from '../../Services/auth.service';
import { ContentService } from '../../Services/content.service';
import { TopNavComponent } from '../shared/top-nav/top-nav.component';
import { Router } from '@angular/router';
import { NavService } from '../../Services/nav.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../Services/toast.service';
import { TaskView } from '../../Models/TaskView';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [StatCardsComponent,TaskItemComponent,TopNavComponent,FormsModule,CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit{
  taskList!:TaskView[];
  activeCount:number=0;
  completedCount:number=0;
  username!:string;
  isToday=true;
  selectedDate:Date=new Date();
  dateText = this.selectedDate.toLocaleDateString('en-US', {
    weekday: 'long',
    day: '2-digit',
    month: 'long',
    year: 'numeric',
  });

  ngOnInit(): void {
    this.navService.activeRouteSignal.set("DashBoard");
    this.loadData();
   
  }
  constructor(public navService:NavService,private contentService:ContentService,private router:Router,public authService:AuthService,private toastService:ToastService)
  {
    this.username=authService.userName;
   this.contentService.dataUpdatedSubject.subscribe({
    next:(val)=>{
      this.loadData();
    }
   })
  }
  loadData(){
    this.contentService.getTasksByDate(this.selectedDate).subscribe({
      next:(data:TaskView[])=>{
        this.taskList=data;
        this.activeCount = this.taskList.filter(task => task.status.toLowerCase() !== 'completed').length;
        this.completedCount=this.taskList.length-this.activeCount;
        
      },
      error:(err)=>{
        this.contentService.handleError(err.statusCode,err.message);
      }
    })

  }
  deleteAll(){
   this.contentService.deleteAll(this.taskList).then(success => {
    if (success) {
      this.toastService.showContrast("Deleted All Tasks");
      this.contentService.dataUpdatedSubject.next(1);
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
