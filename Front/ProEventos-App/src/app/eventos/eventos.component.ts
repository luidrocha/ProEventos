import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

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
  modalRef?: BsModalRef;
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  public larguraImg: number = 150;
  public margemImg: number = 2;

  public exibirImg: boolean = true;

  private _filtroLista: string = '';

  // faz a injeção de dependencia http, Modal e Toastr
  constructor(
    private eventoService: EventoServices,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) {

  }

  // Executa antes de o HTML ser carregado
  public ngOnInit(): void {

    this.spinner.show();

    setTimeout(() => {
      /** spinner ends after 5 seconds */

    }, 5000);


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
          this.eventosFiltrados = this.eventos;
      },
      (error) => console.log(error),
      complete: () => this.spinner.hide()
    )};

  public ExibirImagem(): void {

    this.exibirImg = !this.exibirImg;

  }
// Metodo para janela MODAL
  public openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  // Envia a mensagem do Toastr recebida do serviço
  confirm(): void {

    this.modalRef?.hide();
    this.toastr.success('Evento deletado com Sucesso.', 'Deletado!');

  }

  decline(): void {

    this.modalRef?.hide();
  }

}
