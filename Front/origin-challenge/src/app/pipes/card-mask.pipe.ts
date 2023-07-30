import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cardMask'
})
export class CardMaskPipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): unknown {

    if(value.length <= 4)
      return value;

    var first = value.substring(0, 4);
    var second = value.substring(4, 8);
    var third = value.substring(8, 12);
    var fourth = value.substring(12, 16);

    var result = first + (second.length > 0 ? `-${second}` : "") + (third.length > 0 ? `-${third}` : "") + (fourth.length > 0 ? `-${fourth}` : "");
    return result;
  }

}