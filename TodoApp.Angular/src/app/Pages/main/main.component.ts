import { Component, OnInit, effect, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { NavService } from '../../Services/nav.service';
import { NavBarComponent } from '../../Components/shared/nav-bar/nav-bar.component';
import { AuthService } from '../../Services/auth.service';
import { TaskFormComponent } from '../../Components/task-form/task-form.component';
import {ToastModule} from 'primeng/toast'
import { ToastService } from '../../Services/toast.service';
import { LoaderComponent } from '../../Components/shared/loader/loader.component';
import { CommonModule } from '@angular/common';
import { LoaderService } from '../../Services/loader.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [RouterOutlet,NavBarComponent,TaskFormComponent,ToastModule,CommonModule,LoaderComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss',

})
export class MainComponent{


  constructor(public navService:NavService,public authService:AuthService,private router:Router,public toastService:ToastService,public loaderService:LoaderService,private messageService:MessageService){

    this.authService.AuthenticationFailedSubject.subscribe({
      next:(value)=>{
        {
          if(value){
          this.router.navigate(["../login"]);
          }
        }
      }
    })
    }

}