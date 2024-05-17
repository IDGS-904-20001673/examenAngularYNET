import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, firstValueFrom } from 'rxjs';
import { ProductId} from '../../interfaces/product';
import { buy, userId } from '../../interfaces/auth';
import { GeneralResponse } from '../../interfaces/general-response';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CarritoService {
  private _products = new BehaviorSubject<ProductId[]>([]); 

  constructor(private http: HttpClient) { }

  // Método para actualizar los productos
  putProduct(product: ProductId) {
    const currentProducts = this._products.getValue(); 
    const updatedProducts = [...currentProducts, product]; 
    this._products.next(updatedProducts); 
  }

  // Método para obtener los productos como un observable
  getProducts(): Observable<ProductId[]> {
    return this._products.asObservable(); 
  }

  removeProduct(productId: Number) {
    const currentProducts = this._products.getValue();
    const updatedProducts = currentProducts.filter(product => product.id !== productId);
    this._products.next(updatedProducts);
  }

  async insert(request: buy){
    return await firstValueFrom(this.http.post<GeneralResponse<userId>>(`${environment.api}UserProduct/insert`, request));
  }
}
