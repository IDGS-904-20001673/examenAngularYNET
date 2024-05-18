import { Component } from '@angular/core';
import { NavUserComponent } from '../../components/nav-user/nav-user.component';
import { CarritoService } from '../../core/services/carrito.service';
import { ProductId } from '../../interfaces/product';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { buy, userId } from '../../interfaces/auth';
import { GeneralResponse } from '../../interfaces/general-response';
import { environment } from '../../../environments/environment.development';

@Component({
  selector: 'app-carrito',
  standalone: true,
  imports: [NavUserComponent, CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './carrito.component.html',
  styleUrl: './carrito.component.css'
})
export class CarritoComponent {
  products: ProductId[] = [];
  response: GeneralResponse<userId> = null;
  id : Number;
  success : boolean = false;
  total : any = null;
  rute : string;

  constructor(private serviceCarrito: CarritoService) {
   this.id=Number(sessionStorage.getItem('id'));
   this.rute = environment.api;
  }

  ngOnInit() {
    this.serviceCarrito.getProducts().subscribe(products => {
      this.products = products;
    });
    this.calculateTotal();
  }

  calculateTotal(){
    this.total=0;
    for (const products of this.products) {
      this.total=this.total+products.price;
    }
  }

  removeProduct(productId: Number) {
    this.serviceCarrito.removeProduct(productId);
    this.calculateTotal();
  }

  async submitData() {
    for (const product of this.products) {
      const buy: buy = { 
        idProduct: product.id,
        idCustomer: this.id
      };
      try {
        await this.serviceCarrito.insert(buy);
        this.success=true;
      } catch (error) {
        console.error("Error al enviar la solicitud:", error);
        this.success=false;
        // Puedes manejar el error de alguna otra forma, como mostrar un mensaje al usuario
      }
    }
    if(this.success){
      this.products = [];
      alert("Listo se ah realizado la compra");
      
    }
  }
  


}
