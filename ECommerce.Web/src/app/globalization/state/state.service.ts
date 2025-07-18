import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { StateMainModel, StateParameterModel } from './state.model';
import { HttpService } from '../../services/http.service';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class StateService {

    private apiUrl = environment.apiUrl;

    constructor(private http: HttpService) { }

    public getForLOV(stateParameter: StateParameterModel): Observable<StateMainModel[]> {
        return this.http.post(`${this.apiUrl}state/geLovValueState`, stateParameter).pipe(
          map((response: any) => response?.data ?? response ?? []) // handles both with or without .data
        );
    }

}
