import { CanActivateChildFn, CanActivateFn } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { inject } from '@angular/core';
import { Router } from '@angular/router';

export const authGuard: CanActivateChildFn = (route, state) => {
  let authService=inject(AuthService);

  if(authService.checkCookie()){
      return true;
  }
  else{
  let router=inject(Router);
  return router.createUrlTree(["/login"]);
  }

};
