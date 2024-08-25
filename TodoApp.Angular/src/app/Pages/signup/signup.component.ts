import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { NavService } from '../../Services/nav.service';
import { AuthService } from '../../Services/auth.service';
import { ToastModule } from 'primeng/toast';
import { ToastService } from '../../Services/toast.service';
import { LoginModel } from '../../Models/LoginModel';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [ReactiveFormsModule,RouterLink,ToastModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss'
})
export class SignupComponent {
  constructor(private formBuilder:FormBuilder,private authService:AuthService,private router:Router,private toastService:ToastService){}
  signupForm = this.formBuilder.group({
    username: ["", Validators.required],
    password: ['', [
      Validators.required,  // Ensures password is not empty
      Validators.minLength(8), // Minimum length of 8 characters
      Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])'), // Requires at least one lowercase, uppercase, and digit
    ]],
  });
  onSubmit(){
      this.authService.signUp(this.signupForm.value as LoginModel).subscribe({
        next:(value)=>{
              this.router.navigate(["/dashboard"]);
              this.toastService.showSuccess("User Created!");
            }
      })
  }
  get formValue() {
    return this.signupForm.controls;
  }
  checkValidation() {
    console.log(this.formValue['password'].hasError("required"));
    if(this.formValue['password'].hasError("pattern")){
      this.toastService.showWarn("Password pattern is not matched");

    }
    if(this.formValue['password'].hasError("minlength")){
      this.toastService.showWarn("Password is too short");

    }
    if(this.formValue['password'].hasError("required")){
      this.toastService.showWarn("Password is required");

    }
   
  }
  
}
