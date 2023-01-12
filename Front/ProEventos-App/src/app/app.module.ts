import { NgModule, CUSTOM_ELEMENTS_SCHEMA, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule, FormControl, } from '@angular/forms';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
//import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NgxSpinnerModule } from 'ngx-spinner';

import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale'


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { NavComponent } from './shared/nav/nav.component';
import { DateTimeFormat_Pipe } from './helpers/DateTimeFormatPipe.pipe';
import { TituloComponent } from './shared/Titulo/Titulo.component';
import { DashboardComponent } from './componentes/dashboard/dashboard.component';

import { ContatosComponent } from './componentes/contatos/contatos.component';
import { PalestrantesComponent } from './componentes/palestrantes/palestrantes.component';

import { EventoServices } from './services/eventos.services';
import { EventosComponent } from './componentes/eventos/eventos.component';
import { EventoDetalheComponent } from './componentes/eventos/evento-detalhe/evento-detalhe.component';
import { EventoListaComponent } from './componentes/eventos/evento-lista/evento-lista.component';

import { UserComponent } from './componentes/user/user.component';
import { PerfilComponent } from './componentes/user/perfil/perfil.component';
import { LoginComponent } from './componentes/user/login/login.component';
import { RegistrarComponent } from './componentes/user/registrar/registrar.component';


// configura o idioma do DATEPICKER

defineLocale(`pt-br`, ptBrLocale);




@NgModule({
  declarations: [
    AppComponent,
    EventosComponent,
      PalestrantesComponent,
      NavComponent,
      DateTimeFormat_Pipe,
      TituloComponent,
      PerfilComponent,
      DashboardComponent,
      ContatosComponent,
      EventoDetalheComponent,
      EventoListaComponent,
      UserComponent,
      LoginComponent,
      RegistrarComponent,
     

   ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    ReactiveFormsModule,
    FormsModule,
    

                            // forRoot() foi usado para que se necessário passar parametro
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ToastrModule.forRoot({
                              timeOut: 5000,
                              positionClass: 'toast-bottom-right',
                              preventDuplicates: true,
                              progressBar: true,

                        }),
      NgxSpinnerModule

  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  // Providers => Permite injetar um servico ou classe , nesse caso esta sendo injetado pelo App.module podemos enxergar em qualquer lugar do projeto.
  providers: [EventoServices],
  bootstrap: [AppComponent]
})
export class AppModule { }
