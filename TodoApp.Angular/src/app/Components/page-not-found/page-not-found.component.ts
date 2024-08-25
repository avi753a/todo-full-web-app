import { Component } from '@angular/core';
import { LoginComponent } from '../../Pages/login/login.component';
import { LogoComponent } from '../shared/logo/logo.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-page-not-found',
  standalone: true,
  imports: [LogoComponent,RouterLink],
  templateUrl: './page-not-found.component.html',
  styleUrl: './page-not-found.component.scss'
})
export class PageNotFoundComponent {

}
