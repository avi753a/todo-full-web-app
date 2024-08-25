import { Routes } from '@angular/router';
import { authGuard } from './gaurds/auth.guard';


export const routes: Routes = [
    {path:"",loadComponent:()=>import("./Pages/main/main.component").then(m=>m.MainComponent),canActivateChild:[authGuard],
    children:[
        {path:"dashboard",loadComponent:()=>import("./Components/dashboard/dashboard.component").then(m=>m.DashboardComponent)},
        {path:"active",loadComponent:()=>import("./Components/active/active.component").then(m=>m.ActiveComponent)},
        {path:"completed",loadComponent:()=>import("./Components/completed/completed.component").then(m=>m.CompletedComponent)},
        {path:"",redirectTo:"dashboard",pathMatch:"full"},
    ]
},
{path:"login",loadComponent:()=>import("./Pages/login/login.component").then(m=>m.LoginComponent)},
{path:"signup",loadComponent:()=>import("./Pages/signup/signup.component").then(m=>m.SignupComponent)},
{path:"googleauth",loadComponent:()=>import("./Pages/login-google/login-google.component").then(m=>m.LoginGoogleComponent)},
{path:"**",loadComponent:()=>import("./Components/page-not-found/page-not-found.component").then(m=>m.PageNotFoundComponent)}

   
];
