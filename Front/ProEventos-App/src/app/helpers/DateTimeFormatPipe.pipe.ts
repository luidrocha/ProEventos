import { DatePipe, formatDate } from '@angular/common';
import { LOCALE_ID, Pipe, PipeTransform } from '@angular/core';
import { ptBrLocale } from 'ngx-bootstrap/chronos';
import { Constants } from '../util/Constants';

@Pipe({ name: 'DateTimeFormatPipe' })

// extends DataPipe para fazer o override

export class DateTimeFormat_Pipe  implements PipeTransform {

    
  transform(value: any): any {

   // Constants.DATETIME_FMT
    //let year = new Date(value).getFullYear().toString;
    //let month = new Date(value).getMonth().toString;
    //let day = new Date(value).getDay().toString;

   // let dataFormatada = formatDate(value, 'dd/MM/yyyy hh:mm a','');
    //Constants.DATETIME_FMT

    console.log(value);

    return value;
      

    }
}
