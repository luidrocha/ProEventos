import { Component, Input, OnInit } from '@angular/core';


@Component({
  selector: 'app-Titulo',
  templateUrl: './Titulo.component.html',
  styleUrls: ['./Titulo.component.scss']
})
export class TituloComponent implements OnInit {

  @Input() titulo : any;
  @Input() iconClass = 'fa fa-user';
  @Input() subTitulo = 'Desde 2021';
  @Input() botaoListar = false;

  constructor() {

  }

  ngOnInit() {
  }




}
