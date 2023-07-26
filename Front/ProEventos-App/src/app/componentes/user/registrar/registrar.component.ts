import { Component, OnInit } from '@angular/core';
import {
  AbstractControlOptions,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ValidatorField } from '@app/helpers/ValidatorField';
import { User } from '@app/model/identity/User';
import { AccountService } from '@app/services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styleUrls: ['./registrar.component.scss'],
})
export class RegistrarComponent implements OnInit {

  // usando desta forma não precisa instanciar com new
  user = {} as User;

  constructor(private fb: FormBuilder,
              private accountService: AccountService,
              private router : Router,
              private toaster: ToastrService ) {}

  ngOnInit(): void {
    this.validationRegister();
  }

  formRegister!: FormGroup;

  public get f(): any {
    return this.formRegister.controls;
  }

  public validationRegister(): any {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.validaSenha('password', 'confirmePassword'),
    };

    this.formRegister = this.fb.group(
      {
        primeiroNome: ['', Validators.required],
        ultimoNome: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        userName: ['', Validators.required],
        password: ['', [Validators.required, Validators.minLength(4)]],
        confirmePassword: ['', Validators.required],
        checkOption: [false, Validators.requiredTrue]
      }, formOptions  );
  }


  formDebug() {
    console.log(this.formRegister.value);
  }

  public cssValidator(campoForm: FormControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

register(): void {
// Copia os dados do formulario para o objeto user
  this.user = {...this.formRegister.value}
  this.accountService.register(this.user).subscribe(

    () => {this.router.navigateByUrl('/dashboard')  },
    (error: any) => {
      this.toaster.error(error.error)
    }
  )


}

}
