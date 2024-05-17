import { Component } from '@angular/core';
import { NavUserComponent } from '../../components/nav-user/nav-user.component';
import { ProductService } from '../../core/services/product.service';
import { Observable } from 'rxjs';
import { GeneralResponse } from '../../interfaces/general-response';
import { ProductId } from '../../interfaces/product';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { CarritoService } from '../../core/services/carrito.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products-user',
  standalone: true,
  imports: [NavUserComponent, ReactiveFormsModule, FormsModule, HttpClientModule, CommonModule],
  templateUrl: './products-user.component.html',
  styleUrl: './products-user.component.css'
})
export class ProductsUserComponent {
  response: GeneralResponse<ProductId> = null;
  productsResults$!: Observable<GeneralResponse<ProductId[]>>;
  productSelected: ProductId;
  
  constructor(private service: ProductService, private serviceCarrito: CarritoService,  private route : Router) {

  }
  getProduct(ProductGet: ProductId) {
    this.serviceCarrito.putProduct(ProductGet);
    this.route.navigateByUrl("/carrito");
  }

  ngOnInit(): void {
    this.productsResults$ = this.service.getProducts();
  }

}
