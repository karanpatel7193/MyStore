import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/internal/operators/map';
import { HttpService } from '../../../services/http.service';
import { CityMainModel, CityParameterModel } from './city.model';

@Injectable()
export class StateService {
    constructor(private http: HttpService) {
    }
    public getForLOV(stateParameter: CityParameterModel): Observable<CityMainModel[]> {
        return this.http.post('admin/city/getLovValue', stateParameter).pipe(
            map((response: any) => {
                return response;
            }),
        );
    }


}
