import { Component } from '@angular/core';
import { NewNavComponent } from '../new-nav/new-nav.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-new-main',
  standalone: true,
  imports: [NewNavComponent,RouterOutlet],
  templateUrl: './new-main.component.html',
  styleUrl: './new-main.component.scss'
})
export class NewMainComponent {

}
