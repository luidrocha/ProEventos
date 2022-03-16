import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  /* Cria um objeto Array */
  public eventos: any;
  // faz a injeção de dependencia http
  constructor(private http: HttpClient) {}

  // Executa antes de o HTML ser carregado
  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {

    this.http.get('https://localhost:5001/api/Eventos').subscribe(response => this.eventos=response,
    error => console.log(error));

  }
}
