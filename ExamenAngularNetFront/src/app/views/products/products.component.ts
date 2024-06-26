import { Component } from '@angular/core';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { ProductService } from '../../core/services/product.service';
import { GeneralResponse } from '../../interfaces/general-response';
import { Product, ProductId } from '../../interfaces/product';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';



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
  selectedFile: File | undefined;
  rute : string;

  constructor(private service: ProductService) {
    this.rute = environment.api;
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0]; // Almacena el archivo seleccionado
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
    if (this.formProduct.invalid) {
      return;
    }

    const formData = new FormData();
    formData.append('code', this.formProduct.get('code')?.value);
    formData.append('description', this.formProduct.get('description')?.value);
    formData.append('price', this.formProduct.get('price')?.value);
    formData.append('stock', this.formProduct.get('stock')?.value);
    if (this.selectedFile) {
      formData.append('image', this.selectedFile, this.selectedFile.name);
    }

    try {
      this.response = await this.service.insertProduct(formData);
      if(this.response.status){
        window.location.reload();
      }
    } catch (error) {
      console.error(error);
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
