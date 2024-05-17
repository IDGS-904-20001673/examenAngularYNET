import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';
import { Router } from '@angular/router';
import { login, user } from '../../interfaces/auth';
import { GeneralResponse } from '../../interfaces/general-response';
import { AuthService } from '../../core/services/auth.service';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule,HttpClientModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers: []
})

export class LoginComponent {
  form: FormGroup;
  error : string | null = null;
  success : string | null = null;

  response: GeneralResponse<user> = null;

  constructor( private route : Router,private service: AuthService) {
    this.form = new FormGroup({
      email: new FormControl(),
      password: new FormControl(),
    })

    if (sessionStorage.getItem('error')) {
      this.error = sessionStorage.getItem('error');
      sessionStorage.removeItem('error');
    }
    if (sessionStorage.getItem('success')) {
      this.success = sessionStorage.getItem('success');
      sessionStorage.removeItem('success');
    }
  }

  async submitData() {
    if(this.form.value.email == null || this.form.value.password == null){
      alert("Llena todos los campos");
      return;
      }
    const body = this.form.value as login;   
    this.response = await this.service.login(body);
    if (this.response.value != null) {
      sessionStorage.setItem('id', this.response.value.id+"");
      this.route.navigateByUrl("/productsUser");
    }else{
      this.success=null;
      sessionStorage.setItem('error', 'Inicio de sesion incorrecto, intenta nuevamente');
      window.location.reload();
    }
  }

}


