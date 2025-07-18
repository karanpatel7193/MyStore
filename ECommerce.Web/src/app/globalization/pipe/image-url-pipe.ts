import { Pipe, PipeTransform, inject } from '@angular/core';
import { environment } from '../../../environments/environment';

@Pipe({
  name: 'imageUrlPipe',
  standalone: true
})
export class ImageUrlPipe implements PipeTransform {

  private baseUrl = environment.apiImageUrl;

  transform(value: string): string {
    if (!value) return '';
    return `${this.baseUrl}${value.replace(/\\/g, '/')}`;
  }
}
