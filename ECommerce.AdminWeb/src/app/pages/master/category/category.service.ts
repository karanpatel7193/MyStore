import { Injectable } from '@angular/core';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { HttpService } from 'src/app/services/http.service';
import { CategoryAddModel, CategoryEditModel, CategoryGridModel, CategoryMainModel, CategoryModel, CategoryParameterModel } from './category.model';

@Injectable()
export class CategoryService {
    constructor(private http: HttpService) {
    }

    public getAddMode(categoryParameter: CategoryParameterModel): Observable<CategoryAddModel> {
            return this.http.post('admin/category/getAddMode', categoryParameter).pipe(
                map((response: CategoryAddModel) => {
                    return response;
                }),
            );
        }
    
    public getEditMode(categoryParameter: CategoryParameterModel): Observable<CategoryEditModel> {
        return this.http.post('admin/category/getEditMode', categoryParameter).pipe(
            map((response: CategoryEditModel) => {
                return response;
            }),
        );
    }
    public getRecord(id: number): Observable<CategoryModel> {
        return this.http.get('admin/category/getRecord/' + id).pipe(
            map((response: CategoryModel) => {
                return response;
            }),
        );
    }

    public getParent(): Observable<CategoryModel[]> {
        return this.http.get('admin/category/getParent').pipe(
            map((response: CategoryModel[]) => {
                return response;
            }),
        );
    }

    public getsyncAll(): Observable<CategoryModel[]> {
        return this.http.post('admin/category/getCategoryPropertyValue').pipe(
            map((response: CategoryModel[]) => {
                return response;
            }),
        );
    }

    public getChild(categoryParameter: CategoryParameterModel): Observable<CategoryModel[]> {
        return this.http.post('admin/category/getChild/', categoryParameter).pipe(
            map((response: CategoryModel[]) => {
                return response;
            }),
        );
    }

    public getForLOV(categoryParameter: CategoryParameterModel): Observable<CategoryMainModel[]> {
            return this.http.post('admin/category/getLovValue', categoryParameter).pipe(
                map((response: CategoryMainModel[]) => {
                    return response;
                }),
            );
    }

    public getForGrid(categoryParameter: CategoryParameterModel): Observable<CategoryGridModel> {
        return this.http.post('admin/category/getForGrid', categoryParameter).pipe(
            map((response: CategoryGridModel) => {
                return response;
            }),
        );
    }

    public save(category: CategoryModel): Observable<number> {
        if (category.id === 0)
            return this.http.post('admin/category/insert', category).pipe(
                map((response: number) => {
                    return response;
                }),
            );
        else
            return this.http.post('admin/category/update', category).pipe(
                map((response: number) => {
                    return response;
                }),
            );
    }

    public delete(id: number): Observable<void> {
        return this.http.post('admin/category/delete/' + id, null).pipe(
            map((response: void) => {
                return response;
            }),
        );
    }

}
