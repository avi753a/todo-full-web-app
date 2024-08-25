import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, signal } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
import { LoginModel } from '../Models/LoginModel';
import { ResponceModel } from '../Models/ResponceModel';
import { TokenModel } from '../Models/TokenModel';
import { ToastService } from './toast.service';
import { JwtPayload } from '../Models/JWTPayload';
import { environment } from '../../environments/environment.devolopment';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private AuthPath:string=environment.apiUrl;
  private userCookieName:string="TodoAppUsername";
  private cookieName:string="TodoAppCookie";
  isAuthenticated:boolean=false;
  userName!:string;
  AuthenticationFailedSubject=new BehaviorSubject<boolean>(false);

  constructor(private httpService:HttpClient,private cookieService:CookieService,private toastService:ToastService)
  {
      if(this.cookieService.check(this.userCookieName)){
        this.userName=this.cookieService.get(this.userCookieName);
      }
  }
  login(credentials:LoginModel):Observable<boolean>{
    return this.httpService.post<ResponceModel>(this.AuthPath+"/login",credentials).pipe(
     map(res =>{
        if(res.isSuccess){
            var data:TokenModel=res.value;
            this.setAuthProperties(data.token);
            console.log("Loggedin");
            return true;
        }
        else{
            this.toastService.showError("LoginFailed");
            return false;
        }      
     })
    );
  }
  signUp(credentials:LoginModel):Observable<boolean>{
    return this.httpService.post<ResponceModel>(this.AuthPath+"/Register",credentials).pipe(
      map(res =>{
         if(res.isSuccess){
             var data:TokenModel=res.value;
             this.setAuthProperties(data.token);
             this.toastService.showSuccess("SignUp Successful");
             console.log("Sign Up Success");
             return true;
         }
         else{
             this.toastService.showError("SignUp Failed");
             return false;
         }      
      })
     );

  }
  googlelogin(token:string):Observable<any>{
      var tokenData=new TokenModel(token);
      return this.httpService.post<ResponceModel>(this.AuthPath+"/login/google",tokenData).pipe(
        map(res =>{
           if(res.isSuccess){
               var data:TokenModel=res.value;
               this.setAuthProperties(data.token);
               this.toastService.showSuccess("SignUp Successful");
               console.log("Sign Up Success");
               return true;
           }
           else{
               this.toastService.showError("SignUp Failed");
               return false;
           }      
        })
       );
  }

  setAuthProperties(token:string){
      let claims=this.decodeToken(token);
      if(claims){
        const expires = new Date(claims.exp*1000);
        this.cookieService.set(this.userCookieName,claims.Name,{ expires, path: '/' });
        this.userName=claims.Name;
        this.setCookie(token);
      }

  }
  getToken():string{
    return this.cookieService.get(this.cookieName);
  }

  decodeToken(token:string): JwtPayload|null {
    if (token) {
      console.log(jwtDecode(token));
      return jwtDecode<JwtPayload>(token);
    }
    else{
      return null;
    }
  }
  setCookie(token: string): void {
      const expires = new Date(Date.now() + 5 * 60 * 60 * 1000); // 24 hours * 60 minutes * 60 seconds * 1000 milliseconds
      this.cookieService.set(this.cookieName, token, { expires, path: '/' });
    }
  removeToken(){
    this.cookieService.delete(this.cookieName);
  }
  checkCookie():boolean{
    return this.cookieService.check(this.cookieName);
  }

}
