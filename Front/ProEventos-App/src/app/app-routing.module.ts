import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ContatosComponent } from './componentes/contatos/contatos.component';
import { DashboardComponent } from './componentes/dashboard/dashboard.component';
import { PalestrantesComponent } from './componentes/palestrantes/palestrantes.component';

import { EventosComponent } from './componentes/eventos/eventos.component';
import { EventoDetalheComponent } from './componentes/eventos/evento-detalhe/evento-detalhe.component';
import { EventoListaComponent } from './componentes/eventos/evento-lista/evento-lista.component';

import { UserComponent } from './componentes/user/user.component';
import { PerfilComponent } from './componentes/user/perfil/perfil.component';
import { LoginComponent } from './componentes/user/login/login.component';
import { RegistrarComponent } from './componentes/user/registrar/registrar.component';

// Colocamos nossas rotas aqui

const routes: Routes = [
{path: 'user', component: UserComponent,
children: [
  {path: 'login', component: LoginComponent},
  {path: 'registrar', component: RegistrarComponent},
]
},
{path: 'user/perfil', component: PerfilComponent},
{path: 'eventos', redirectTo: 'eventos/listar'},
{path: 'eventos', component: EventosComponent,
children:[

  /* Sub rotas de eventos, rotas filhas  criadas dentro do component principal EVENTOS*/
  {path:'detalhe/:id', component: EventoDetalheComponent},
  {path: 'detalhe', component: EventoDetalheComponent},
  {path : 'listar', component: EventoListaComponent},

]
},


{path: 'dashboard', component: DashboardComponent},
{path: 'palestrantes', component: PalestrantesComponent},
{path: 'perfil', component : PerfilComponent},
{path: 'contatos', component: ContatosComponent},
{path: '', redirectTo: 'dashboard', pathMatch: 'full'},
{path: '**', redirectTo: 'dashboard', pathMatch :'full'}
];




@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
