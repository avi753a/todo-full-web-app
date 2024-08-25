import { HttpClient } from '@angular/common/http';
import { Injectable, computed, signal } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { ToastService } from './toast.service';
import { PriorityView } from '../Models/PriorityView';
import { StatusView } from '../Models/StatusView';
import { TaskRegistration } from '../Models/TaskRegistration';
import { TaskView } from '../Models/TaskView';
import { environment } from '../../environments/environment.devolopment';
import { ResponceModel } from '../Models/ResponceModel';
import { DateFormat } from '../Models/DateFormat';
import { AuthService } from './auth.service';
import { CookieService } from 'ngx-cookie-service';
import { TaskDetails } from '../Models/TaskDetails';

@Injectable({
  providedIn: 'root'
})
export class ContentService {
  taskList!: TaskView[];
  PriorityList!: PriorityView[];
  StatusList!: StatusView[];
  editTaskSubject = new BehaviorSubject<string>("");
  dataUpdatedSubject = new BehaviorSubject<number>(0);

  constructor(private http: HttpClient, private toastService: ToastService,private authService:AuthService) {
    this.getAllPriorites();
    this.getAllStatus();
  }
  private path = environment.apiUrl + "/Tasks";
  private statusPath = environment.apiUrl + "/Status"
  private priorityPath = environment.apiUrl + "/Priority";



  addTask(task: TaskRegistration): Observable<boolean> {
    return this.http.post<ResponceModel>(this.path, task).pipe(
      map(res => {
        if (res.isSuccess) {
          return res.value;
        }
        else{
          this.handleError(res.statusCode,res.message);
        }
      })
    )
  }
  editTask(task: TaskRegistration, id: string): Observable<boolean> {
    return this.http.put<ResponceModel>(this.path + "/" + id, task).pipe(
      map(res => {
        if (res.isSuccess) {
          return res.value;
        }
        else{
          this.handleError(res.statusCode,res.message);
        }
      })
    )
  }
  getTasksByDate(date: Date): Observable<TaskView[]> {
    var dateSting:string=date.toISOString();
    var dateInput = new DateFormat(dateSting);
    return this.http.post<ResponceModel>(this.path + "/TasksByDate", dateInput).pipe(
      map(res => {
        if (res.isSuccess) {
          return res.value;
        }
        else{
          this.handleError(res.statusCode,res.message);
        }
      })
    )
  }
  getAllPriorites(): Observable<PriorityView[]> {
    return this.http.get<ResponceModel>(this.priorityPath).pipe(
      map(res => {
        if (res.isSuccess) {
          return res.value;
        }
        else{
          this.handleError(res.statusCode,res.message);
        }
      })
    )
    
  }

  convertToRegistration(task: TaskView) {
    let statusId = this.StatusList.find(s => s.name === task.status)?.id as number;
    let priorityId = this.PriorityList.find(p => p.name === task.priority)?.id as number;
    return new TaskRegistration(task.name, task.description, priorityId, statusId);
  }

  getAllStatus(): Observable<StatusView[]> {
    return this.http.get<ResponceModel>(this.statusPath).pipe(
      map(res => {
        if (res.isSuccess) {
          return res.value
        }
        else{
          this.handleError(res.statusCode,res.message);
        }
      }))
  }
  getTaskDetails(id:string):Observable<TaskDetails>{
    return this.http.get<ResponceModel>(this.path+"/"+id+"/Details").pipe(
      map(res => {
        if (res.isSuccess) {
          return res.value
        }
        else{
          this.handleError(res.statusCode,res.message);
        }
      }))
  }
  deleteTask(id: string) {
    return this.http.delete<ResponceModel>(this.path + '/' + id).pipe(
      map(res=>{
        if(res.isSuccess){
          return res.value;
        }
        else{
          this.handleError(res.statusCode,res.message);
        }
      })
      
    );
  }
  deleteAll(list: TaskView[]): Promise<boolean> {
    const deletePromises = list.map(element => 
        this.deleteTask(element.id).toPromise()
    );
    return Promise.all(deletePromises)
        .then(() => true) 
        .catch(() => false); 
}
  handleError(statusCode:number,message:string) {
    console.log("handling error",statusCode,message);
    switch(statusCode){
      case 401:()=>{
         this.toastService.showError("Authorization Error! Login again");
         this.authService.AuthenticationFailedSubject.next(true);
         this.authService.removeToken();
         
      }
    }
  }
  DateFilter(list: TaskView[], date: Date): TaskView[] {
    return list.filter(t => {
      const createdDate = new Date(t.createdTime);
      const selectedDate = new Date(date);

      const sameYear = createdDate.getUTCFullYear() === selectedDate.getUTCFullYear();
      const sameMonth = createdDate.getUTCMonth() === selectedDate.getUTCMonth();
      const sameDate = createdDate.getDate() === selectedDate.getDate();
      console.log(createdDate, selectedDate, sameYear, sameMonth, sameDate, createdDate.getDate(), selectedDate.getDate());
      return sameYear && sameMonth && sameDate;

    });
  }
}




// priority-view.ts

