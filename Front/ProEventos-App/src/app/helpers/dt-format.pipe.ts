import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from '../util/Constants';
import { Moment } from 'moment';

@Pipe({
  name: 'dtFormat'
})
export class DtFormatPipe implements PipeTransform {

  transform(value: any, Constats_DATETIME_FMT): any {
    return value;
  }

}
