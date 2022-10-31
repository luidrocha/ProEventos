import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent implements OnInit {
  form!: FormGroup;

  constructor(private fb: FormBuilder,  private localeService: BsLocaleService) {

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
}
