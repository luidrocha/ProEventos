
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../model/Evento';
import { take } from 'rxjs';

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
    // recebe quantas emissões você quer escutar. No caso de take(1),
    // queremos apenas o primeiro valor emitido pelo Observable
    return this.http
      .get<Evento[]>(this.baseURL)
      .pipe(take(1));
  
  }

  // retorna uma Array
  public getEventosByTema(tema: string): Observable<Evento[]> {

    return this.http
      .get<Evento[]>('${ this.baseURL }/{tema}/tema')
      .pipe(take(1));

  }

  // retorna um obj

  public getEventoById(id: number): Observable<Evento> {

    //const teste = this.http.get<Evento>(`${this.baseURL}/{'id'}`);
    //console.log(teste);

    return this.http
      .get<Evento>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  //  passa, grava o evento e retorna o Evento
  public post(evento: Evento): Observable<Evento> {
    return this.http
      .post<Evento>(this.baseURL, evento)
      .pipe(take(1));
  }

  // Faz a atualização da informação no caso, Evento

  public put(evento: Evento): Observable<Evento> {
    return this.http
      .put<Evento>(`${this.baseURL}/${evento.id}`, evento)
      .pipe(take(1));
  }

  // Delete recebe de retorno uma string informando se foi deletado ou não

  public deleteEvento(id: number, ): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }


}
