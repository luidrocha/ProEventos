import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
//import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NgxSpinnerModule } from 'ngx-spinner';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { EventoServices } from './services/eventos.services';
import { EventosComponent } from './componentes/eventos/eventos.component';
import { PalestrantesComponent } from './componentes/palestrantes/palestrantes.component';
import { NavComponent } from './shared/nav/nav.component';
import { DateTimeFormat_Pipe } from './helpers/DateTimeFormatPipe.pipe';
import { TituloComponent } from './shared/Titulo/Titulo.component';
import { PerfilComponent } from './componentes/perfil/perfil.component';
import { DashboardComponent } from './componentes/dashboard/dashboard.component';
import { ContatosComponent } from './componentes/contatos/contatos.component';





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
      ContatosComponent
   ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    FormsModule,
                            // forRoot() foi usado para que se necessário passar parametro
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
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
