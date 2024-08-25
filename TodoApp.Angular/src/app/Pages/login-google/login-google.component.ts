declare var google:any;
import { Component ,OnInit} from '@angular/core';
import { AuthService } from '../../Services/auth.service';
import { ToastService } from '../../Services/toast.service';
import { Router, RouterLink } from '@angular/router';
import { TokenModel } from '../../Models/TokenModel';
@Component({
  selector: 'app-login-google',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './login-google.component.html',
  styleUrl: './login-google.component.scss'
})
export class LoginGoogleComponent implements OnInit{
  ngOnInit(): void {
    console.log("Init");
    google.accounts.id.initialize({
      client_id: '582335238323-d2oh6buftkt31kgue4ioaa7nntltbfq0.apps.googleusercontent.com',
      callback: (res:any)=>{
        console.log(res);
        this.OnGoogleResponce(res);
      }
    });
    google.accounts.id.prompt();
    google.accounts.id.renderButton(document.getElementById("goolge-btn"),{
      theme:"filled_white",
      size:"large",
      shape:"pill",
    });
  }
  constructor(private authService:AuthService,private toastService:ToastService,private router:Router){
      
  }
  OnGoogleResponce(responce:any){
    console.log(responce.credentials);
      this.authService.googlelogin(responce.credential as string).subscribe({
          next:(value:TokenModel)=>{
            this.authService.setAuthProperties(value.token);
            console.log(value.token);
            this.toastService.showSuccess("Login Successful")
            this.router.navigate(["/dashboard"]);
        },
        error:(err)=>{
          this.toastService.showError("Login Failed");
        }
      })
  }
}
