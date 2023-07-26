import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';

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

import { AuthGuard } from './guard/auth.guard';

// Colocamos nossas rotas aqui

const routes: Routes = [

  // Todos os eventos vão aparecer no home, não vai precisar de autenticação. Qualquer usuario podera se inscrever
  {path: '', redirectTo: 'home', pathMatch: 'full'},

// Colocando todas as demas rotas como FILHAS, assim usareos uma unica referencia do AuthGuard
// Criamos um agrupamentos. Todos os filhos devem ser autenticados
{
  path:'',
  runGuardsAndResolvers:'always',
  canActivate: [AuthGuard],
  children: [
              {path: 'user', redirectTo: 'user/perfil'},
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
              {path: 'palestrantes', component: PalestrantesComponent },
              {path: 'perfil', component : PerfilComponent},
              {path: 'contatos', component: ContatosComponent },

            ]
},

// fim da configuração Authguard


{path: 'user', component: UserComponent,
children: [
  {path: 'login', component: LoginComponent},
  {path: 'registrar', component: RegistrarComponent},
]
},

{path: 'home', component: HomeComponent},
// Qualquer endereço não autenticado vai para home
{path: '**', redirectTo: 'home', pathMatch :'full'}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
