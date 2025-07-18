import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BlockModel } from '../home/block/block.model';

@Injectable({
  providedIn: 'root'
})
export class SliderService {

  constructor(private http: HttpClient) { }
  private apiUrl = 'https://localhost:7143/client/home';

  public getForSlider(): Observable<BlockModel[]> {
    return this.http.post<BlockModel[]>(`${this.apiUrl}/getSlider`, {});
  }
}
