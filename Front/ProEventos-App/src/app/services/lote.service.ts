
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../model/Evento';
import { take } from 'rxjs';
import { Lote } from '../model/Lote';
import { EventoDetalheComponent } from '@app/componentes/eventos/evento-detalhe/evento-detalhe.component';
import { TemplateBindingParseResult } from '@angular/compiler';

//Permite injetar um servico ou classe , nesse caso esta sendo injetado no Root, assim, qualquer parte da aplicação
// Enxerga e poderá ser usado em outros modulos.

//  {providedIn: 'root'} colocado entre  parente para injetar no Root o serviço.

@Injectable()

// Um serviço também é uma classe, a diferença é que quando usamos o nome Service, temos a intenção de usar esta classe
// para prover recursos em outros lugares, por isso adotamos a terminação Service.

export class LoteServices {

   //baseURL = 'https://localhost:44301/api/lotes';

  private readonly baseURL='https://localhost:5001/api/lotes';

  constructor(private http: HttpClient ) { }


  // retorna uma Array

  public getLotesByEventoId(eventoId: number): Observable<Lote[]> {
    // recebe quantas emissões você quer escutar. No caso de take(1),
    // queremos apenas o primeiro valor emitido pelo Observable
    return this.http
      .get<Lote[]>(`${this.baseURL}/${eventoId}`)
      .pipe(take(1));

  }

  // Faz a atualização da informação no caso, Evento

  public saveLote(eventoId: number, lotes: Lote[]): Observable<Lote> {

    return this.http
    .put<Lote>(`${this.baseURL}/${eventoId}`, lotes)
      .pipe(take(1));


  }

  // Delete recebe de retorno uma string informando se foi deletado ou não

  public deleteLote(eventoId: number, loteId: number ): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${eventoId}/${loteId}`)
      .pipe(take(1));
  }


}
