import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { AccountService } from '@app/services/account.service';
import { User } from '@app/model/identity/User';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService) {}

  intercept( request: HttpRequest<unknown>, next: HttpHandler ): Observable<HttpEvent<unknown>> {

    let currentUser: User;

    // Vai la no serviço e ver se o usuário está logado e retorna o usuário.
    this.accountService.currentUser$
      .pipe(take(1))
      .subscribe(user => {
        currentUser = user
        // Se ficasse fora do subscribe retornaria o token antes do resultado do subscribe
        // Se tiver usuario logado, faz o clone do http e inclui o header com token do usuario
       // Equivale a mandar a requisição novamente com a nova requisição
        if (currentUser) {
          request = request.clone({
            setHeaders: {
              Authorization: `Bearer ${currentUser.token}`,
            },
          });
        }

      });
    return next.handle(request);
  }
}
