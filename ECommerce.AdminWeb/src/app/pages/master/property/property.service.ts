import { Injectable } from '@angular/core';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { HttpService } from 'src/app/services/http.service';
import { PropertyGridModel, PropertyModel, PropertyParameterModel } from './property.model';

@Injectable()
export class PropertyService {
    constructor(private http: HttpService) {
    }

    public getRecord(id: number): Observable<PropertyModel> {
        return this.http.get('admin/property/getRecord/' + id).pipe(
            map((response: PropertyModel) => {
                return response;
            }),
        );
    }

    public getForLOV(propertyParameter: PropertyParameterModel): Observable<PropertyModel[]> {
        return this.http.post('admin/property/getLovValue', propertyParameter).pipe(
            map((response: PropertyModel[]) => {
                return response;
            }),
        );
    }

    public getForGrid(propertyParameter: PropertyParameterModel): Observable<PropertyGridModel> {
        return this.http.post('admin/property/getGridData', propertyParameter).pipe(
            map((response: PropertyGridModel) => {
                return response;
            }),
        );
    }

    public save(property: PropertyModel): Observable<number> {
        if (property.id === 0)
            return this.http.post('admin/property/insert', property).pipe(
                map((response: number) => {
                    return response;
                }),
            );
        else
            return this.http.post('admin/property/update', property).pipe(
                map((response: number) => {
                    return response;
                }),
            );
    }

    public delete(id: number): Observable<void> {
        return this.http.post('admin/property/delete/' + id, null).pipe(
            map((response: void) => {
                return response;
            }),
        );
    }

}
