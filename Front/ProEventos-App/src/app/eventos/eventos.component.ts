import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { filter } from 'rxjs';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {

  /* Cria um objeto Array */
  public eventos: any;
  public eventosFiltrados: any = [];

  larguraImg: number = 150;
  margemImg: number = 2;


  exibirImg: boolean = true;

  private _filtroLista: string ='';

  // faz a injeção de dependencia http
  constructor(private http: HttpClient) {

  }

  // Executa antes de o HTML ser carregado
  ngOnInit(): void {
    this.getEventos();
  }

  // Pegar os valores digitados para o filtro

  public get FiltrarLista(): string{
    return this._filtroLista;
  }

  // Setar os valores digitados para pesquisa

  public set FiltrarLista(value : string){

    this._filtroLista = value;
    this.eventosFiltrados = this.FiltrarLista ? this.FiltrarEventos(this.FiltrarLista):this.eventos

  }

  FiltrarEventos(filtrarPor: string) :any {

    filtrarPor = filtrarPor.toLocaleLowerCase();

    //  indexOf = -1 equivale a não encontrar
    //  Filtra pelo tema ou pelo local

    return this.eventos.filter(
      (evento: any )=> evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
       evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1 )


  }


  public getEventos(): void {
   this.http.get('https://localhost:5001/api/Eventos').subscribe(
      (response) => {
        this.eventos = response,
      this.eventosFiltrados = this.eventos},
      (error) => console.log(error)
    );
  }

  ExibirImagem() {

    this.exibirImg = !this.exibirImg;

  }
}
