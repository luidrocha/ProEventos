import { Component, OnInit } from '@angular/core';
import {
  AbstractControlOptions,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styleUrls: ['./registrar.component.scss'],
})
export class RegistrarComponent implements OnInit {

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.validationRegister();
  }

  formRegister!: FormGroup;

  public get f(): any {
    return this.formRegister.controls;
  }

  public validationRegister(): any {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.validaSenha('senha', 'confirmeSenha'),
    };

    this.formRegister = this.fb.group(
      {
        fnome: ['', Validators.required],
        lnome: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        usuario: ['', Validators.required],
        senha: ['', [Validators.required, Validators.minLength(6)]],
        confirmeSenha: ['', Validators.required],
        checkOption: ['', Validators.required]
      }, formOptions  );
  }
  formDebug() {
    console.log(this.formRegister.value);
  }

  public cssValidator(campoForm: FormControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }
}
