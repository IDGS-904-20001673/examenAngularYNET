import { Component } from '@angular/core';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { ProductService } from '../../core/services/product.service';
import { GeneralResponse } from '../../interfaces/general-response';
import { Product, ProductId } from '../../interfaces/product';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';



@Component({
  selector: 'app-products',
  standalone: true,
  imports: [NavbarComponent, ReactiveFormsModule, FormsModule, HttpClientModule, CommonModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {

  response: GeneralResponse<ProductId> = null;
  productsResults$!: Observable<GeneralResponse<ProductId[]>>;
  productSelected: ProductId;

  constructor(private service: ProductService) {

  }

  formProduct = new FormGroup({
    code: new FormControl(),
    description: new FormControl(),
    price: new FormControl(),
    img: new FormControl(),
    stock: new FormControl(),
  })

  ngOnInit(): void {
    this.productsResults$ = this.service.getProducts();
  }
 
  async submitData() {
    if(this.formProduct.value.code == "" || this.formProduct.value.description == "" || this.formProduct.value.price == "" || this.formProduct.value.stock == ""){
      alert("Llena todos los campos");
      return;
      }
    const body = this.formProduct.value as Product;   
    this.response = await this.service.insertProduct(body);
    if (this.response.status) {
      window.location.reload();
    }
  }

  getProduct(ProductGet: ProductId) {
    this.productSelected = ProductGet;

  }

  async updateData() {
    const body = this.formProduct.value as ProductId;

    body.id = this.productSelected.id;
    this.response = await this.service.UpdateProduct(body);
    if (this.response.status) {
      window.location.reload();
    }
  }

  async DeleteData(id : Number) {
    this.response = await this.service.deleteProduct(id);
    if (this.response.status) {
      window.location.reload();
    }
  }

}
