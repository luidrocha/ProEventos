import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '@app/model/identity/User';
import { AccountService } from '@app/services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  isCollapsed = true;
  constructor( private router: Router,
              public accountService: AccountService)  { }

  ngOnInit() : void {
  }

  // Esconde o menu se o usuario estiver na tela de login.

  showMenu(): boolean {


    return this.router.url != '/user/login'
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/dashboard')
  }

}
