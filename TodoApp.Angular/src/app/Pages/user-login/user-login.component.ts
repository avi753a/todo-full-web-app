import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ToastModule } from 'primeng/toast';
import { AuthService, LoginModel } from '../../Services/auth.service';
import { ToastService } from '../../Services/toast.service';

@Component({
  selector: 'app-user-login',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,RouterLink,ToastModule],
  templateUrl: './user-login.component.html',
  styleUrl: './user-login.component.scss'
})
export class UserLoginComponent {
  @Input() isSignUp:boolean=false;
  constructor(private formBuilder:FormBuilder,private authService:AuthService,private router:Router,private toastService:ToastService){}
  loginForm=this.formBuilder.group({
    username:["",Validators.required],
    password: ['', [
      Validators.required,  // Ensures password is not empty
      Validators.minLength(8), // Minimum length of 8 characters
      Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])'), // Requires at least one lowercase, uppercase, and digit
    ]],
  });
  onSubmit(){
    if(this.isSignUp){
      this.authService.signUp(this.loginForm.value as LoginModel).subscribe({
        next:(value)=>{
          this.authService.login(this.loginForm.value as LoginModel).subscribe({
            next:(value)=>{
              this.authService.setAuthProperties(value);
              this.router.navigate(["/dashboard"]);
            }
          })
        }
      })
      return;
    }
    this.authService.login(this.loginForm.value as LoginModel).subscribe({
      next:(value)=>{
          this.authService.setAuthProperties(value as string);
          this.router.navigate(["/dashboard"]);
      }
    });
    
  }
  get formValue() {
    return this.loginForm.controls;
  }
  checkValidation(event: any) {
    console.log(this.formValue['password'].errors);
    console.log(this.formValue['password'].hasError("required"));
    if(this.formValue['password'].hasError("pattern")){
      this.toastService.showError("Password pattern is not Matched");
      console.log("error called");

    }

    if(this.formValue['password'].hasError("required") && this.formValue['password'].touched){
      this.toastService.showError("Password is required");
      console.log("error called");
    }
   
  }
  toggleForm(){
    this.isSignUp=!this.isSignUp;
  }
  
}
