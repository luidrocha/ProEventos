import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

//Biblioteca externa
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ActivatedRoute } from '@angular/router';

import { Evento } from '@app/model/Evento';
import { EventoServices } from '@app/services/eventos.services';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent implements OnInit {
  form!: FormGroup;
  estadoSalvar = 'post';

  // inicia a variavel com um obj vazio do tipo Evento
  evento = {} as Evento;


  constructor(private fb: FormBuilder,
    private localeService: BsLocaleService,
    private router: ActivatedRoute,
    private activatedRouter: ActivatedRoute,
    private eventoService: EventoServices,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) {

    //private router: ActivatedRoute => p/usado para pegar a rota/com id

    // Passa a conf para o serviço do DatePcker configurar o portuges.
    // Passa a conf para o serviço do DatePcker configurar o portuges.

    this.localeService.use('pt-br')
  }

  // Retorna o comando Form.control
  get f(): any {
    return this.form.controls;
  }

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
  public cssValidator(campoForm: FormControl): any {

    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  ngOnInit(): void {
    this.carregarEvento();
    this.validationForm();
  }

  public validationForm(): void {
    this.form = this.fb.group({
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(50),
        ],
      ],
      local: ['', [Validators.required, Validators.maxLength(30)]],
      dataEvento: ['', Validators.required],
      qtdPessoa: ['', [Validators.required, Validators.max(500)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemUrl: ['', Validators.required],
    });


  }

  public carregarEvento(): void {
    const paramId = this.router.snapshot.paramMap.get('id');
    console.log("Captura " + paramId);
    if (paramId !== null) {
      this.estadoSalvar = 'put';
      this.spinner.show();
      // + converte para numerico
      this.eventoService.getEventoById(+paramId).subscribe({

        // Object.assign({}, evento)} =>  para fazer uma copia do objeto ou para o objeto
        // ... (stred)
        next: (evento: Evento) => {
          this.evento = { ...evento },
            this.form.patchValue(this.evento)
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

  public salvarAlteracao(): void {

    if (this.estadoSalvar=='post') {
            if (this.form.valid) {
              // Verifica se o formulario é valido e usando o spread (...) copia
              // usando o spread substitui todo o valor de evento, até o id.
              this.evento = { ...this.form.value }

              this.spinner.show();
              this.eventoService.postEvento(this.evento).subscribe(
                () => { this.toastr.success(`Evento salvo com sucesso`, `Sucesso!`) },
                (error: any) => {
                  console.error(error);
                  this.spinner.hide();
                  this.toastr.error(`Erro ao salvar o evento `, `Erro!`);

                },
                () => { this.spinner.hide(); }
              )
            }
    } else {

      if (this.form.valid) {
        // Verifica se o formulario é valido e usando o spread (...) copia
        // essa linha diz para adicionar um id que vem do evento.id
        this.evento = {id: this.evento.id, ...this.form.value }

        this.spinner.show();
        this.eventoService.putEvento(this.evento.id, this.evento).subscribe(
          () => { this.toastr.success(`Evento atualizado com sucesso`, `Sucesso!`) },
          (error: any) => {
            console.error(error);
            this.spinner.hide();
            this.toastr.error(`Erro ao atualizar o evento `, `Erro!`);

          },
          () => { this.spinner.hide(); }
        )
      }
      }
   

  }
}
