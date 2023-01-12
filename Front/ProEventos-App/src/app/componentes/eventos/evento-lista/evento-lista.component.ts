import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';


import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { filter } from 'rxjs';
import { Evento } from '@app/model/Evento';
import { EventoServices } from '@app/services/eventos.services';





@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})

// Executa antes de o HTML ser carregado
export class EventoListaComponent implements OnInit {

  /* Cria um objeto Array */
  modalRef?: BsModalRef;
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public eventoId = 0;

  public larguraImg: number = 150;
  public margemImg: number = 2;

  public exibirImg: boolean = true;

  private _filtroLista: string = '';

  // faz a injeção de dependencia http, Modal e Toastr
  constructor(
    private eventoService: EventoServices,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router) { }

  ngOnInit(): void {

    this.spinner.show();
    this.CarregarEventos();
  }

  // Pegar os valores digitados para o filtro

  public get FiltrarLista(): string {
    return this._filtroLista;
  }

  // Setar os valores digitados para pesquisa

  public set FiltrarLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.FiltrarLista
      ? this.FiltrarEventos(this.FiltrarLista)
      : this.eventos;
  }

  public FiltrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();

    //  indexOf = -1 equivale a não encontrar
    //  Filtra pelo tema ou pelo local

    return this.eventos.filter(
      (evento: any) =>
        evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  public CarregarEventos(): void {
    this.eventoService.getEventos().subscribe(

       (eventos: Evento[]) => {
        (this.eventos = eventos);
        (this.eventosFiltrados = this.eventos);
      },
       (error: any) => {
        // this.spinner.hide();
        this.toastr.error('Erro ao Carregar os Eventos', 'Erro !');

      },

      // complete: () => this.spinner.hide()
    ).add(() => this.spinner.hide());
  };

  public ExibirImagem(): void {
    this.exibirImg = !this.exibirImg;
  }
  // Metodo para janela MODAL
  public openModal(event: any, template: TemplateRef<any>, eventoid: number) {
    //faz com que o click da linha de editar não propague para o Botão EXCLUIR

    event.stopPropagation();
    this.eventoId = eventoid;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  // Envia a mensagem do Toastr recebida do serviço
  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.eventoService.deleteEvento(this.eventoId).subscribe(
      (result: any) => {
        if (result.message == 'Deletado')
           console.log(result);
        this.toastr.success(`O Evento foi deletado com Sucesso.`, `Deletado!`);
       // this.spinner.hide();
        this.CarregarEventos();
        },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o Evento ${this.eventoId}`, 'Erro!');
       // this.spinner.hide();
      }
      // Add chama o Hide após cada execução do bloco fazendo o Complete(), se executado . 
      //complete: () => { this.spinner.hide(); } substituido por  .add(() =>  this.spinner.hide())
    ).add(() =>  this.spinner.hide());
  }
  

  decline(): void {
    this.modalRef?.hide();
  }

  // pega o id da linha clicada.
  detalheEvento(id: number): void {

    this.router.navigate([`eventos/detalhe/${id}`]);

  }

}
