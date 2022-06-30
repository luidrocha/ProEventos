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
import { EventosComponent } from './eventos/eventos.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { NavComponent } from './nav/nav.component';
import { EventoServices } from './services/eventos.services';
import { DateTimeFormat_Pipe } from './helpers/DateTimeFormatPipe.pipe';


@NgModule({
  declarations: [
    AppComponent,
    EventosComponent,
      PalestrantesComponent,
      NavComponent,
      DateTimeFormat_Pipe
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
                              timeOut: 50000,
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
