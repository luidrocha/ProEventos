import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';


@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  formPerfil! : FormGroup;

  constructor( private fb : FormBuilder, ) { }

  ngOnInit() {

    this.validationPerfil();
  }

  public get f() : any {

    return this.formPerfil.controls;
  }

  public validationPerfil() : any {

    const formOptions : AbstractControlOptions = {

    validators: ValidatorField.validaSenha('senha', 'confirmeSenha'),

    };

    this.formPerfil = this.fb.group(
      {
      fnome : ['', [Validators.required, Validators.minLength(5)]],
      lnome : ['',Validators.required],
      email : ['', [Validators.required, Validators.email]],
      telefone :['', [Validators.required, Validators.minLength(12)]],
      descricao: ['', Validators.required],
      senha: ['', [Validators.required, Validators.minLength(6)]],
      confirmeSenha: ['', [Validators.required, Validators.minLength(6)]],
    }
    ), formOptions;


    }

  }

