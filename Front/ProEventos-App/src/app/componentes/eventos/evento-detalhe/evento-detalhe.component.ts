import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent implements OnInit {
  form!: FormGroup;

  // Retorna o comando Form.control
  get f(): any {
    return this.form.controls;
  }

  resetForm(): void {
    this.form.reset();
  }
  constructor(private fb: FormBuilder) {}

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
          Validators.maxLength(70),
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
