import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/internal/operators/map';
import { HttpService } from '../../../services/http.service';
import { StateParameterModel, StateMainModel } from './state.model';

@Injectable()
export class StateService {
    constructor(private http: HttpService) {
    }
    public getForLOV(stateParameter: StateParameterModel): Observable<StateMainModel[]> {
        return this.http.post('admin/state/getLovValue', stateParameter).pipe(
            map((response: any) => {
                return response;
            }),
        );
    }


}
