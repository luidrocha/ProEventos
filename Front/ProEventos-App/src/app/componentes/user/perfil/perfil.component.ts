import { Component, OnInit } from '@angular/core';
import {
  AbstractControlOptions,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ValidatorField } from '@app/helpers/ValidatorField';
import { User } from '@app/model/identity/User';
import { UserUpdate } from '@app/model/identity/UserUpdate';
import { AccountService } from '@app/services/account.service';
import { NgxSpinnerService, Spinner } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss'],
})
export class PerfilComponent implements OnInit {
  formPerfil!: FormGroup;
  // objeto global
  userUpdate = {} as UserUpdate;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private toaster: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit() {
    this.validationPerfil();

    this.carregarUsuario();
  }

   carregarUsuario(): void {
    this.spinner.show();
    this.accountService.getUser().subscribe(
        (userRetorno: UserUpdate) => {
          console.log(JSON.stringify(userRetorno));
          this.userUpdate = userRetorno
          this.formPerfil.patchValue(this.userUpdate)
          this.toaster.success('Usuário carregado', 'Sucesso !')
        },
        (erro: any) => {
          console.log(erro)
          this.toaster.error('Erro ao carregar usuário', 'Erro !')
          this.router.navigate(['/dasboard']);

        },
      ).add(() => this.spinner.hide())
  }

public atualizarUsuario() {

    this.userUpdate = {...this.formPerfil.value}
    this.spinner.show();

    this.accountService.updateUser(this.userUpdate).subscribe(
      () =>  {this.toaster.show('Usuário atualizado !', 'Sucesso !')
      this.spinner.hide()},
      (error) => {
        this.toaster.error(error.error);
        this.spinner.hide();
        console.log(error);
      }
    )
  }

  public get f(): any {
    return this.formPerfil.controls;
  }

  public validationPerfil(): any {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.validaSenha('password', 'confirmePassword'),
    };

    this.formPerfil = this.fb.group(
      {
        //Esse campo só é usado para obeter o username ao carregar o formulario. Não é requerido
        userName: [''],
        titulo: ['NaoInformado', Validators.required],
        primeiroNome: ['', Validators.required],
        ultimoNome: [ '', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        phoneNumber: ['', [Validators.required, Validators.minLength(12)]],
        descricao: ['', Validators.required],
        funcao: ['NaoInformado', Validators.required],
        password: ['',  Validators.minLength(6)],
        confirmePassword: ['', Validators.minLength(6)],
      },
      formOptions
    );
  }

  onSubmit(): void {
    if (this.formPerfil.invalid) {
      return;
    }
  }
  public formReset(event: any): void {
    // Evita fazer refresh para da o reset.

    event.preventDefault();
    this.formPerfil.reset();
  }
}
