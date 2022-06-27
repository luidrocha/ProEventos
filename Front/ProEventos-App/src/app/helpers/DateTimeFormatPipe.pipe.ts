import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from '../util/Constants';

@Pipe({
  name: 'DateTimeFormatPipe'
})
// extends DataPipe para fazer o override

export class DateTimeFormat_Pipe extends  DatePipe implements PipeTransform {

  override transform(value: any, args?: any): any {
    return super.transform(value, Constants.DATETIME_FMT);
  }

}
