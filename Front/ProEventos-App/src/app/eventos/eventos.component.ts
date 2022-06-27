import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { filter } from 'rxjs';
import { Evento } from '../model/Evento';
import { EventoServices } from '../services/eventos.services';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {

  /* Cria um objeto Array */
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  public larguraImg: number = 150;
  public margemImg: number = 2;

  public exibirImg: boolean = true;

  private _filtroLista: string = '';

  // faz a injeção de dependencia http
  constructor(private eventoService: EventoServices) {

  }

  // Executa antes de o HTML ser carregado
  public ngOnInit(): void {
    this.getEventos();
  }

  // Pegar os valores digitados para o filtro

  public get FiltrarLista(): string {
    return this._filtroLista;
  }

  // Setar os valores digitados para pesquisa

  public set FiltrarLista(value: string) {

    this._filtroLista = value;
    this.eventosFiltrados = this.FiltrarLista ? this.FiltrarEventos(this.FiltrarLista) : this.eventos

  }

  public FiltrarEventos(filtrarPor: string): Evento[] {

    filtrarPor = filtrarPor.toLocaleLowerCase();

    //  indexOf = -1 equivale a não encontrar
    //  Filtra pelo tema ou pelo local

    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1)


  }


  public getEventos(): void {
    this.eventoService.getEventos().subscribe(
      (eventos: Evento[]) => {
        this.eventos = eventos,
          this.eventosFiltrados = this.eventos
      },
      (error) => console.log(error)
    );
  }

  public ExibirImagem(): void {

    this.exibirImg = !this.exibirImg;

  }
}
