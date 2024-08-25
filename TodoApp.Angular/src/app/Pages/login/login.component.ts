import { Component, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { NavService } from '../../Services/nav.service';
import { AuthService } from '../../Services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { ToastService } from '../../Services/toast.service';
import { ToastModule } from 'primeng/toast';
import { LoginModel } from '../../Models/LoginModel';
import { TokenModel } from '../../Models/TokenModel';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule,RouterLink,ToastModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent{
  constructor(private formBuilder:FormBuilder,private authService:AuthService,private router:Router,private toastService:ToastService){
  }
  loginForm=this.formBuilder.group({
    username:["",Validators.required,Validators.minLength(4)],
    password: ['', [
      Validators.required,  // Ensures password is not empty
      Validators.minLength(8), // Minimum length of 8 characters
      Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])'), // Requires at least one lowercase, uppercase, and digit
    ]],
  });

  onSubmit(){
    this.authService.login(this.loginForm.value as LoginModel).subscribe({
      next:(value:boolean)=>{
          this.router.navigate(["/dashboard"]);
          this.toastService.showSuccess("Login Successful");

      }
    });
  }
  get formValue() {
    return this.loginForm.controls;
  }
  checkValidation() {
    if(this.formValue['password'].hasError("pattern") && this.formValue['password'].touched){
      this.toastService.showWarn("Password pattern is not Matched");
    }
    if(this.formValue['password'].hasError("minlength")){
      this.toastService.showWarn("Password is too short");
    }
    if(this.formValue['password'].hasError("required")){
      this.toastService.showWarn("Password is required");
    }
    if(this.formValue['username'].hasError("required")){
      this.toastService.showWarn("Username is required");
    }
    if(this.formValue['username'].hasError("minlength")){
      this.toastService.showWarn("Username is too short");
    }
  }
  
}
