import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpService } from '../../../services/http.service';
import { CategoryPropertyAddModel, CategoryPropertyEditModel, CategoryPropertyGridModel, CategoryPropertyListModel, categoryPropertyMainModel, CategoryPropertyModel, CategoryPropertyParameterModel } from './category-property.model';

@Injectable()
export class CategoryPropertyService {
    constructor(private http: HttpService) { }

    public getRecord(id: number): Observable<CategoryPropertyModel> {
        return this.http.get('admin/CategoryProperty/getRecord/' + id).pipe(
            map((response: CategoryPropertyModel) => response)
        );
    }

    public getForLOV(categoryPropertyParameterModel: CategoryPropertyParameterModel): Observable<categoryPropertyMainModel[]> {
		return this.http.post('admin/categoryProperty/getForLOV', categoryPropertyParameterModel).pipe(
			map((response: categoryPropertyMainModel[]) => {
				return response;
			}),
		);
	}

    public getForGrid(categoryPropertyParameterModel: CategoryPropertyParameterModel): Observable<CategoryPropertyGridModel> {
        return this.http.post('admin/categoryProperty/getForGrid', categoryPropertyParameterModel).pipe(
            map((response: CategoryPropertyGridModel) => response)
        );
    }

    public save(categoryPropertyEntity: CategoryPropertyModel, mode: string): Observable<number> {
        if (mode === 'Add') {
            return this.http.post('admin/CategoryProperty/insert', categoryPropertyEntity).pipe(
                map((response: number) => response)
            );
        } else {
            return this.http.post('admin/CategoryProperty/update', categoryPropertyEntity).pipe(
                map((response: number) => response)
            );
        }
    }

    public delete(id: number): Observable<void> {
        return this.http.post(`Admin/CategoryProperty/delete/${id}`, null).pipe(
            map(() => { })
        );
    }

    public getAddMode(categoryPropertyParameterModel: CategoryPropertyParameterModel): Observable<CategoryPropertyAddModel> {
            return this.http.post('admin/CategoryProperty/getForAdd', categoryPropertyParameterModel).pipe(
                map((response: CategoryPropertyAddModel) => {
                    return response;
                }),
            );
        }
    
        public getEditMode(categoryPropertyParameterModel: CategoryPropertyParameterModel): Observable<CategoryPropertyEditModel> {
            return this.http.post('admin/CategoryProperty/getForEdit', categoryPropertyParameterModel).pipe(
                map((response: CategoryPropertyEditModel) => {
                    return response;
                })
            );
        }
    
        public getListMode(categoryPropertyParameterModel: CategoryPropertyParameterModel): Observable<CategoryPropertyListModel> {
            return this.http.post('admin/CategoryProperty/getForList', categoryPropertyParameterModel).pipe(
                map((response: CategoryPropertyListModel) => {
                    return response;
                }),
            );
        }
        public getForCategoryProperty(categoryPropertyParameterModel: CategoryPropertyParameterModel): Observable<CategoryPropertyModel[]> {
            return this.http.post('admin/categoryProperty/getCategoryProperty', categoryPropertyParameterModel).pipe(
                map((response: CategoryPropertyModel[]) => {
                    return response;
                }),
            );
        }
}
