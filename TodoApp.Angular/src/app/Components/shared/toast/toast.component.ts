import { Component, OnInit } from '@angular/core';
import { ContentService } from '../../../Services/content.service';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule,BrowserAnimationsModule],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.scss',
  // animations:[    
  //   trigger('toastTrigger', [     
  //     state('open', style({ transform: 'translateY(0%)' })), 
  //     state('close', style({ transform: 'translateY(-200%)' })), 
  //     transition('open <=> close', [    
  //       animate('300ms ease-in-out')
  //     ])    
  //   ])  
  // ]
})
export class ToastComponent implements OnInit{

  toastClass = ['toast-class']; // Class lists can be added as an array  
  toastMessage = 'This is a toast';  
  showsToast = true;

  constructor() { }  

 
  ngOnInit(): void {    
    setTimeout(() => {      
      this.showsToast = true;    
    }, 1000);  
  }
}
