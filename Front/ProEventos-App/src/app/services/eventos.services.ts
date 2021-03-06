
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../model/Evento';

//Permite injetar um servico ou classe , nesse caso esta sendo injetado no Root, assim, qualquer parte da aplicação
// Enxerga e poderá ser usado em outros modulos.

//  {providedIn: 'root'} colocado entre  parente para injetar no Root o serviço.

@Injectable()

// Um serviço também é uma classe, a diferença é que quando usamos o nome Service, temos a intenção de usar esta classe
// para prover recursos em outros lugares, por isso adotamos a terminação Service.

export class EventoServices {

// baseURL = 'https://localhost:44301/api/Eventos';
  baseURL ='https://localhost:5001/api/Eventos';

  constructor(private http: HttpClient) { }

  // retorna uma Array

  public getEventos(): Observable<Evento[]> {

    return this.http.get<Evento[]>(this.baseURL)
  }

  // retorna uma Array
  public getEventosByTema(tema: string): Observable<Evento[]> {

    return this.http.get<Evento[]>('${ this.baseURL }/{tema}/tema');

  }

  // retorna um obj

  public getEventoById(id: number): Observable<Evento> {

    return this.http.get<Evento>('${this.baseURL}/{id/id');
  }


}
