import { DatePipe, formatDate } from '@angular/common';
import { LOCALE_ID, Pipe, PipeTransform } from '@angular/core';
import { ptBrLocale } from 'ngx-bootstrap/chronos';
import { Constants } from '../util/Constants';


@Pipe({ name: 'DateFormatPipe' })

// extends DataPipe para fazer o override

export class DateTimeFormat_Pipe  extends DatePipe implements PipeTransform {


  override transform(value: any, args?: any): any {

    return super.transform(value, Constants.DATETIME_FMT);


    //Constants.DATETIME_FMT

    console.log(value);

    return value;


    }
}
