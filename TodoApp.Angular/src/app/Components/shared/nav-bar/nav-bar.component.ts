import { Component, effect, inject } from '@angular/core';
import { NavLink, NavService } from '../../../Services/nav.service';
import { LogoComponent } from '../logo/logo.component';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { eventNames } from 'process';
import { AuthService } from '../../../Services/auth.service';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [LogoComponent,RouterLink,RouterLinkActive,CommonModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent {
  selectedValue!:string;
  activeNavigation!:string;
  constructor(private navService:NavService,private router:Router,private authService:AuthService){
    effect(()=>{
      this.activeNavigation=navService.activeRouteSignal();
    })
  }
  navLinks:NavLink[]=this.navService.navLinks;
  showForm(){
    this.navService.toggleForm();
  }
  onNavigationSelect(e:any){
    this.router.navigate([e.target?.value]);
  }
  logout(){
    this.authService.removeToken();
    this.router.navigate(["login"]);
   }
}

