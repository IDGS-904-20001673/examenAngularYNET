import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductShopService } from '../../core/services/product.Shop.service';
import { ProductShop, ProductShopId } from '../../interfaces/productShop';
import { GeneralResponse } from '../../interfaces/general-response';
import { Observable } from 'rxjs';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ShopService } from '../../core/services/shop.service';
import { ProductService } from '../../core/services/product.service';
import { ProductId } from '../../interfaces/product';
import { ShopId } from '../../interfaces/shop';

@Component({
  selector: 'app-product-shop',
  standalone: true,
  imports: [NavbarComponent, ReactiveFormsModule, FormsModule, HttpClientModule, CommonModule], 
  templateUrl: './product-shop.component.html',
  styleUrl: './product-shop.component.css'
})
export class ProductShopComponent {

  response: GeneralResponse<ProductShopId> = null;
  productShopsResults$!: Observable<GeneralResponse<ProductShopId[]>>;
  shopSelected: ProductShopId;

  productsResults$!: Observable<GeneralResponse<ProductId[]>>;
  shopsResults$!: Observable<GeneralResponse<ShopId[]>>;

  constructor(private service: ProductShopService, private serviceShop: ShopService, private serviceProduct: ProductService ) {

  }

  formShop = new FormGroup({
    idShop: new FormControl(null, [Validators.required]),
    idProduct: new FormControl(null, [Validators.required]),
  });
  ngOnInit(): void {
    this.productShopsResults$ = this.service.getAll();
    this.shopsResults$ = this.serviceShop.getAll();
    this.productsResults$ = this.serviceProduct.getProducts();
  }
 
  async submitData() {
    if(this.formShop.value.idProduct == null || this.formShop.value.idShop == null){
    alert("Llena todos los campos");
    return;
    }
    const body = this.formShop.value as ProductShop;   
    this.response = await this.service.insert(body);
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
