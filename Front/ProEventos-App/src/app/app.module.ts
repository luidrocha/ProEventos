import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CollapseModule } from 'ngx-bootstrap/collapse';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
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
    FormsModule

  ],
  // Providers => Permite injetar um servico ou classe , nesse caso esta sendo injetado pelo App.module podemos enxergar em qualquer lugar do projeto.
  providers: [EventoServices],
  bootstrap: [AppComponent]
})
export class AppModule { }
