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
export class MyBuyService {

  constructor(private http: HttpClient) { }


  getAll(): Observable<GeneralResponse<userId[]>>{
    return this.http.get<GeneralResponse<userId[]>>(`${environment.api}UserProduct/getAll`);
  }

}
