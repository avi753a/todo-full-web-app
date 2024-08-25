import { ApplicationConfig, contentChild, inject } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { HttpHandlerFn, HttpRequest, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { AuthService } from './Services/auth.service';
import { provideAnimations } from '@angular/platform-browser/animations';
import { MessageService } from 'primeng/api';
import { finalize } from 'rxjs';
import { LoaderService } from './Services/loader.service';
import { ToastService } from './Services/toast.service';

export const appConfig: ApplicationConfig = {

  providers: [provideRouter(routes),provideAnimations(), provideClientHydration(),provideHttpClient(withFetch(),withInterceptors([authInterceptor])),MessageService,ToastService]
};
export function authInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
  let authService=inject(AuthService);
  let loaderService=inject(LoaderService);
  let authToken=authService.getToken();
  loaderService.showLoader(); 
  if (authToken) {
    const cloned = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${authToken}`)
    });   
    return next(cloned).pipe(
      finalize(() => loaderService.hideLoader())
    );
  } else {
    return next(req).pipe(
      finalize(() => loaderService.hideLoader())
    );
  }
 
}