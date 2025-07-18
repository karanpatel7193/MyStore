import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SearchGridModel, SearchProductParameterModel, SearchPropertyMongoModel } from './search.model';
import { HttpService } from '../../services/http.service';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class SearchService {
    private baseUrl = environment.apiUrl;

    constructor(private http: HttpService) { }

    public getForCriteriea(categoryId: number): Observable<SearchGridModel> {
        return this.http.post(`${this.baseUrl}search/getForCriteriea?CategoryId=${categoryId}`, {}).pipe(
            map((response: SearchGridModel) => {
                return response || new SearchGridModel();
            }),
        );
    }

    public getForSearch(searchParameterModel: SearchProductParameterModel): Observable<SearchPropertyMongoModel[]> {
        return this.http.post(`${this.baseUrl}search/getForSearch`, searchParameterModel).pipe(
            map((response: SearchPropertyMongoModel[]) => {
                return response;
            }),
        );
    }
}

