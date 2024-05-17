import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product, ProductId } from '../../interfaces/product';
import { Observable, firstValueFrom } from 'rxjs';
import { GeneralResponse } from '../../interfaces/general-response';
import { environment } from '../../../environments/environment.development';
import { ProductShop, ProductShopId } from '../../interfaces/productShop';

@Injectable({
  providedIn: 'root'
})
export class ProductShopService {

  constructor(private http : HttpClient) { }

  async insert(request: ProductShop){
    return await firstValueFrom(this.http.post<GeneralResponse<ProductShopId>>(`${environment.api}productShops/insert`, request));
  }
  getAll(): Observable<GeneralResponse<ProductShopId[]>>{
    return this.http.get<GeneralResponse<ProductShopId[]>>(`${environment.api}productShops/getAll`);
  }
  async delete(id: Number){
    return await firstValueFrom(this.http.delete<GeneralResponse<ProductShopId>>(`${environment.api}productShops/delete/`+id));
  }
}
