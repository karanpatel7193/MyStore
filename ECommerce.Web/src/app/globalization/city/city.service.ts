import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CityMainModel, CityParameterModel } from './city.model';
import { environment } from '../../../environments/environment';
import { HttpService } from '../../services/http.service';

@Injectable({
    providedIn: 'root'
})
export class CityService {

    private apiUrl = environment.apiUrl;

    constructor(private http: HttpService) { }

    public getForLOV(cityParameter: CityParameterModel): Observable<CityMainModel[]> {
        return this.http.post(`${this.apiUrl}city/getLovValueCity`, cityParameter).pipe(
            map((response: any) => response?.data ?? response ?? []) // handles both with or without .data
        );
    }

}
