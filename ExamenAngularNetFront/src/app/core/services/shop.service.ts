import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product, ProductId } from '../../interfaces/product';
import { Observable, firstValueFrom } from 'rxjs';
import { GeneralResponse } from '../../interfaces/general-response';
import { environment } from '../../../environments/environment.development';
import { Shop, ShopId } from '../../interfaces/shop';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http : HttpClient) { }

  async insert(request: Shop){
    return await firstValueFrom(this.http.post<GeneralResponse<ShopId>>(`${environment.api}shops/insert`, request));
  }
  getAll(): Observable<GeneralResponse<ShopId[]>>{
    return this.http.get<GeneralResponse<ShopId[]>>(`${environment.api}shops/getAll`);
  }
  async Update(request: ShopId){
    return await firstValueFrom(this.http.put<GeneralResponse<ShopId>>(`${environment.api}shops/update`, request));
  }
  async delete(id: Number){
    return await firstValueFrom(this.http.delete<GeneralResponse<ShopId>>(`${environment.api}shops/delete/`+id));
  }
}
