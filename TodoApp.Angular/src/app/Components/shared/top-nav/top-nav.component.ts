import { Component, Input } from '@angular/core';
import { LogoComponent } from '../logo/logo.component';
import { NavLink, NavService } from '../../../Services/nav.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';

@Component({
  selector: 'app-top-nav',
  standalone: true,
  imports: [LogoComponent],
  templateUrl: './top-nav.component.html',
  styleUrl: './top-nav.component.scss'
})
export class TopNavComponent {
  @Input() PageName="";


 constructor(private navService:NavService,private router:Router,private authService:AuthService){}
 navList:NavLink[]=this.navService.navLinks;
 logout(){
  this.authService.removeToken();
  this.router.navigate(["/login"]);
 }
}
