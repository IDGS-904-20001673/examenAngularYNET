import { HttpInterceptorFn } from '@angular/common/http';
import { finalize, map } from 'rxjs';
import { LoaderService } from '../services/loader.service';
import { inject } from '@angular/core';

export const loaderInterceptor: HttpInterceptorFn = (req, next) => {
  const loader = inject(LoaderService);
  
  loader.show();
  return next(req).pipe(finalize(() => {

    setTimeout(()=>{
      loader.hide();
    }, 500);
  }));
};
