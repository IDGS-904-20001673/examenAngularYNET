import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product, ProductId } from '../../interfaces/product';
import { Observable, firstValueFrom } from 'rxjs';
import { GeneralResponse } from '../../interfaces/general-response';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http : HttpClient) { }

  async insertProduct(request: Product){
    return await firstValueFrom(this.http.post<GeneralResponse<ProductId>>(`${environment.api}products/insert`, request));
  }
  getProducts(): Observable<GeneralResponse<ProductId[]>>{
    return this.http.get<GeneralResponse<ProductId[]>>(`${environment.api}products/getAll`);
  }
  async UpdateProduct(request: ProductId){
    return await firstValueFrom(this.http.put<GeneralResponse<ProductId>>(`${environment.api}products/update`, request));
  }
  async deleteProduct(id: Number){
    return await firstValueFrom(this.http.delete<GeneralResponse<ProductId>>(`${environment.api}products/delete/`+id));
  }
}
