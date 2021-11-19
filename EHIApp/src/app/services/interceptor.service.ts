import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class InterceptorService implements HttpInterceptor {
   intercept(req: HttpRequest<any>, next: HttpHandler):   Observable<HttpEvent<any>> {
       // All HTTP requests are going to go through this method
       if (!req.headers.has('Content-Type')) {
        req = req.clone({
          headers: req.headers.set('Content-Type', 'application/json')
        });
      }
       return next.handle(req);
   }
}
