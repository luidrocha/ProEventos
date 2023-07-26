import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '@app/model/identity/User';
import { environment } from '@enviroments/environment';
import { Observable, ReplaySubject, map, take } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  // currentUserSource variavel do tipo observable, toda vez que sofer modificação também atualiza currentUser$
  // Recebe diversas atualizações, toda vez que logar, atualizar perfil tem que atualizar o token

  public currentUserSource = new ReplaySubject<User>(1);
  //variavel do tipo observe ou subject tem o $ no final aponta para pos de memória  currentUserSource
  // como currentUser$ é do tipo observable outros objetos podem se escrever nele
  public currentUser$ = this.currentUserSource.asObservable();

  baseURL = environment.apiURL + '/api/account/';

  constructor(private http: HttpClient) {}

  // map faz o mapeamento do retorno para response do tipo User

  public login(model: any): Observable<void> {
    return this.http.post<User>(this.baseURL + 'login', model).pipe(
      take(1),
      map((response: User) => {
        const user = response;
        if (user) {
          // Grava o usuario logado . JSON converte o obj para string
          localStorage.setItem('user', JSON.stringify(user));

          this.setCurrentUser(user);
        }
      })
    );
  }

  public register(model: any): Observable<void> {
    return this.http.post<User>(this.baseURL + 'register', model).pipe(
      take(1),
      map((response: User) => {
        const user = response;
        if (user) {
          // Grava o usuario logado . JSON converte o obj para string
          localStorage.setItem('user', JSON.stringify(user));

          this.setCurrentUser(user);
        }
      })
    );
  }

  public logout(): void {
    localStorage.removeItem('user');
    // Todos os objetos inscritos no  currentUser$ receberão a atualização
    this.currentUserSource.next(null);
    // Ja termina a subscrição informando que não vai mais receber mudanças no currentUser$
    //this.currentUserSource.complete();
  }

  public setCurrentUser(user): void {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
    console.log(this.currentUser$);
  }
}
