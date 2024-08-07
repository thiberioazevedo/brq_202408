import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor() {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            // if ([401, 403].includes(err.status) && this.accountService.userValue) {
            //     // auto logout if 401 or 403 response returned from api
            //     this.accountService.logout();
            // }

            let error : any = undefined;
            if (err?.error?.errors)
            {
                let obj: any;
                if (err?.error?.errors?.$values)
                    obj = err?.error?.errors?.$values;
                else
                    obj = err?.error?.errors;

                Object.keys(obj).map((val: any)=> error = `${error ?  error + '<br>' : ''}${obj[val]} `)
            }
            else
                error = err.message || err.error?.message || err.statusText;

            console.error(err);
            return throwError(() => error);
        }))
    }
}