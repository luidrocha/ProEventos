import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder,
         FormControl, FormGroup, Validators } from '@angular/forms';

//Biblioteca externa
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ActivatedRoute, Router } from '@angular/router';

import { Evento } from '@app/model/Evento';
import { Lote } from '../../../model/Lote';
import { EventoServices } from '@app/services/eventos.services';
import { LoteServices } from '@app/services/lote.service';
import { ValueConverter } from '@angular/compiler/src/render3/view/template';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})

export class EventoDetalheComponent implements OnInit {
  form!: FormGroup;
  // Se o botão novo for clicado carrega o form e o
  estadoSalvar = 'post';
  eventoId : number;
  // inicia a variavel com um obj vazio do tipo Evento
  evento = {} as Evento;


  constructor(private fb: FormBuilder,
    private localeService: BsLocaleService,
    private eventoService: EventoServices,
    private loteService: LoteServices,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private activatedRouter: ActivatedRoute,
    private router: Router) {


    //private router: ActivatedRoute => p/usado para pegar a rota/com id

    // Passa a conf para o serviço do DatePcker configurar o portuges.
    // Passa a conf para o serviço do DatePcker configurar o portuges.

    this.localeService.use('pt-br')
  }

  get modoEditar() : boolean {
    return this.estadoSalvar=='put';
  }

  // Retorna o comando Form.control
 public  get f(): any {
    return this.form.controls;
  }

  // faz as configurações do DatePicker
  get dpConfig(): any {

    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
      showTodayButton: true
    }
  }

  public resetForm(): void {
    this.form.reset();
  }

  ngOnInit(): void {
    this.carregarEvento();
    this.validationForm();

  }
  get lotes() : FormArray {

    return this.form.get('lotes') as FormArray

  }
  cssValidator(campoForm: FormControl | AbstractControl | null): any {

    return { 'is-invalid': campoForm?.errors && campoForm?.touched };

  }

  public validationForm(): void {
    this.form = this.fb.group({
      tema: ['',
      [ Validators.required,Validators.minLength(5),
        Validators.maxLength(50), ],
      ],
      local: ['', [Validators.required, Validators.maxLength(30)]],
      dataEvento: ['', Validators.required],
      qtdPessoa: ['', [Validators.required, Validators.max(500)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemUrl: ['', Validators.required],
      // Recebe um array fazio
      lotes: this.fb.array([]),
    });
  }


  public criarLote(lote: Lote): FormGroup {

    return   this.fb.group({
      id: [lote.id],
      nome: [lote.nome, Validators.required],
      preco: [lote.preco, Validators.required],
      dataInicio: [lote.dataInicio],
      dataFim: [lote.dataFim],
      quantidade: [lote.quantidade, Validators.required],

    });
  }

  public carregarEvento(): void {
    this.eventoId = +this.activatedRouter.snapshot.paramMap.get('id');
    console.log("Captura " + this.eventoId);

    if (this.eventoId !== null && this.eventoId !== 0) {

      this.estadoSalvar = 'put'; // put = Update, Atualizar
      this.spinner.show();
      // + converte para numerico
      this.eventoService.getEventoById(+this.eventoId).subscribe({

        // Object.assign({}, evento)} =>  para fazer uma copia do objeto ou para o objeto
        // ... (stred)
        next: (evento: Evento) => {
          this.evento = { ...evento };
          this.form.patchValue(this.evento);
          this.carregarLotes();
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao tentar carregar Evento', 'Erro!');
          console.log(error);
        },
        complete: () =>
        this.spinner.hide(),

      });
    }
  }

public carregarLotes() : void {
  this.loteService.getLotesByEventoId(this.eventoId).subscribe(
    (lotesRetorno:Lote[])=>  {
      // this.lotes.push é o GET LOTES. lote representa cada lote do loteRetorno
      lotesRetorno.forEach( lote => {this.lotes.push(this.criarLote(lote));
      });
    },
    (error: any)=> {
      this.toastr.error('Erro ao tentar carregar lotes','Erro !');
      console.error(error);
    }
  ).add(() => this.spinner.hide());

}


  public salvarEvento(): void {
    this.spinner.show();
    // Verifica se o formulario é valido e usando o spread (...) copia
    // usando o spread substitui todo o valor de evento, até o id.
    if (this.form.valid) {
      this.evento = (this.estadoSalvar == 'post')
        ? { ...this.form.value }
        : { id: this.evento.id, ...this.form.value };
      // passa o estado como post
      this.eventoService[this.estadoSalvar](this.evento).subscribe(
        (eventoRetorno: Evento) =>
              { this.toastr.success(`Evento salvo com sucesso`, `Sucesso!`);
      // Faz o reload do caminho editar e disponibiliza a opção cadastrar lote
              this.router.navigate([`eventos/detalhe/${eventoRetorno.id}`]);
              },

        (error : any) => {
                          console.error(error);
                          this.spinner.hide();
                          this.toastr.error(`Erro ao salvar o evento `, `Erro!`);
                          },
        () =>  this.spinner.hide()
    );

    }
  }

  public salvarLotes(): void {

    this.spinner.show();

    if(this.form.controls['lotes'].valid) {
      this.loteService.saveLote(this.eventoId, this.form.value.lotes).subscribe(
        () => {
                this.toastr.success('Lotes salvos com Sucesso!', 'Sucesso!');
                //this.lotes.reset();
               },
        (error: any) => {
                this.toastr.error('Erro ao tentar salvar lotes',' Error!');
                console.error(error);
                console.log(this.form.value.lotes) ;
              },

      ).add(() => this.spinner.hide());
    }

  }

  public adicionarLote(): void {
    // faz um cast pra FormArray   //(this.form.get('lotes') as FormArray).push(this.criarLote(lote));
    // this.lotes chama o get lotes
    //{ id: 0 } as Lote) primeiro item vai ser zero e os demais valores padrões

    this.lotes.push(this.criarLote({ id: 0 } as Lote));

    console.log( this.lotes);
   }

}
