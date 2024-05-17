import { Component } from '@angular/core';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { Shop, ShopId } from '../../interfaces/shop';
import { GeneralResponse } from '../../interfaces/general-response';
import { Observable } from 'rxjs';
import { ShopService } from '../../core/services/shop.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [NavbarComponent, ReactiveFormsModule, FormsModule, HttpClientModule, CommonModule],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css'
})
export class ShopComponent {

  response: GeneralResponse<ShopId> = null;
  shopsResults$!: Observable<GeneralResponse<ShopId[]>>;
  shopSelected: ShopId;

  constructor(private service: ShopService) {

  }

  formShop = new FormGroup({
    branch: new FormControl(),
    address: new FormControl(),
  })

  ngOnInit(): void {
    this.shopsResults$ = this.service.getAll();
  }
 
  async submitData() {
    if(this.formShop.value.branch == "" || this.formShop.value.address == "" ){
      alert("Llena todos los campos");
      return;
      }
    const body = this.formShop.value as Shop;   
    this.response = await this.service.insert(body);
    if (this.response.status) {
      window.location.reload();
    }
  }

  getShop(shopGet: ShopId) {
    this.shopSelected = shopGet;

  }

  async updateData() {
    const body = this.formShop.value as ShopId;

    body.id = this.shopSelected.id;
    this.response = await this.service.Update(body);
    if (this.response.status) {
      window.location.reload();
    }
  }

  async DeleteData(id : Number) {
    this.response = await this.service.delete(id);
    if (this.response.status) {
      window.location.reload();
    }
  }
}
