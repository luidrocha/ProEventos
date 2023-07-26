import { Component, Inject, LOCALE_ID } from '@angular/core';
import { AccountService } from './services/account.service';
import { User } from './model/identity/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ProEventos-App';

  // para manter o stado do Serviço de login do usuário foi criado esta rotina, pois, mesmo o usuario logado
  // não estava apresentando o menu. Se o usuario digita-se a url e desse enter

  constructor(public accountService: AccountService) {}

// sempre vai da um REFRESH no currentUser
ngOnInit(): void {
this.setCurrentUser();
}

setCurrentUser() : void {

  let user = User;

  if (localStorage.getItem('user')) {
  // atribui o user caso exista , senão ?? atribui vazio.
    user = JSON.parse(localStorage.getItem('user') ?? '{}');
  }   else {
    user= null;
  }

  if (user)
  this.accountService.setCurrentUser(user);

}

}

