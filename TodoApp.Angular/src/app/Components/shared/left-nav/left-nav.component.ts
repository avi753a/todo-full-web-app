import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NavLink, NavService } from '../../../Services/nav.service';
import { CommonModule } from '@angular/common';
import { LogoComponent } from '../logo/logo.component';

@Component({
  selector: 'app-left-nav',
  standalone: true,
  imports: [RouterLink,RouterLinkActive,CommonModule,LogoComponent],
  templateUrl: './left-nav.component.html',
  styleUrl: './left-nav.component.scss'
})
export class LeftNavComponent {
constructor(private navService:NavService){

}
navLinks:NavLink[]=this.navService.navLinks;
showForm(){
  this.navService.toggleForm();
}
}
