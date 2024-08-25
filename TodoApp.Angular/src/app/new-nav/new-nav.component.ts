import { Component } from '@angular/core';
import { LogoComponent } from '../Components/logo/logo.component';
import { NavLink, NavService } from '../Services/nav.service';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-new-nav',
  standalone: true,
  imports: [LogoComponent,RouterLink,RouterLinkActive,CommonModule],
  templateUrl: './new-nav.component.html',
  styleUrl: './new-nav.component.scss'
})
export class NewNavComponent {
  constructor(private navService:NavService){

  }
  navLinks:NavLink[]=this.navService.navLinks;
  showForm(){
    this.navService.toggleForm();
  }
}
