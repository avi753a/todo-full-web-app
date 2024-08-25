import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavService {

  constructor() { }
  isFormDisplay:boolean=false;
  activeRouteSignal=signal("");
  
  toggleForm(){
    this.isFormDisplay=!this.isFormDisplay;
  }
  navLinks:NavLink[]=[
    new NavLink("DashBoard","./dashboard"),
    new NavLink("Active","./active"),
    new NavLink("Completed","./completed")
  ];
}
export  class NavLink{
  constructor(public name:string,public path:string){}
}
