import { Component } from '@angular/core';
import { MyBuyService } from '../../core/services/myBuys.service';
import { userId } from '../../interfaces/auth';
import { NavUserComponent } from '../../components/nav-user/nav-user.component';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { GeneralResponse } from '../../interfaces/general-response';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-my-buys',
  standalone: true,
  imports: [NavUserComponent, CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './my-buys.component.html',
  styleUrl: './my-buys.component.css'
})
export class MyBuysComponent {
  user: userId[] = [];
  myBuys: userId[] = []; // Variable para almacenar los userIds con idCustomer igual a 1
  response: Observable<GeneralResponse<userId[]>> = null;
  id : Number;
  constructor(private service: MyBuyService) {
    this.id=Number(sessionStorage.getItem('id'));
  }

  ngOnInit(): void {
    this.getAllUsers();
    
  }

  myBuysfun() {
    this.myBuys = [];
    for (const user of this.user) {
      if (user.idCustomer === this.id ) {
        this.myBuys.push(user);
      }
    }
  }

  getAllUsers() {
    this.service.getAll().subscribe(
      (response) => {
        this.user = response.value;
        this.myBuysfun()
      },
      (error) => {
        console.error('Error al obtener los userIds:', error);
      }
    );
  }
}
