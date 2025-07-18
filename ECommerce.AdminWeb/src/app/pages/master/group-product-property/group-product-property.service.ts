import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpService } from '../../../services/http.service';
import { GroupProductPropertyAddModel, GroupProductPropertyEditModel, GroupProductPropertyGridModel, GroupProductPropertyMainModel, GroupProductPropertyModel, GroupProductPropertyParameterModel } from './group-product-property.model';

@Injectable()
export class GroupProductPropertyService {

    constructor(private http: HttpService) { }

    public getRecord(id: number): Observable<GroupProductPropertyModel> {
        return this.http.get('admin/groupProductProperty/getRecord/' + id).pipe(
            map((response: GroupProductPropertyModel) => response)
        );
    }

    public getForLOV(parameter: GroupProductPropertyParameterModel): Observable<GroupProductPropertyMainModel[]> {
        return this.http.post('admin/groupProductProperty/getForLOV', parameter).pipe(
            map((response: GroupProductPropertyMainModel[]) => response)
        );
    }

    public getForGrid(groupProductPropertyParameterModel: GroupProductPropertyParameterModel): Observable<GroupProductPropertyGridModel> {
        return this.http.post('admin/groupProductProperty/getForGrid', groupProductPropertyParameterModel).pipe(
            map((response: GroupProductPropertyGridModel) => response)
        );
    }

    public save(groupProductPropertyEntity: GroupProductPropertyModel, mode: string): Observable<number> {
        if (mode === 'Add') {
            return this.http.post('admin/groupProductProperty/insert', groupProductPropertyEntity).pipe(
                map((response: number) => response)
            );
        } else {
            return this.http.post('admin/groupProductProperty/update', groupProductPropertyEntity).pipe(
                map((response: number) => response)
            );
        }
    }

    public delete(id: number): Observable<void> {
        return this.http.post(`admin/groupProductProperty/delete/${id}`, null).pipe(
            map(() => { })
        );
    }

    public getAddMode(parameter: GroupProductPropertyParameterModel): Observable<GroupProductPropertyAddModel> {
        return this.http.post('admin/groupProductProperty/getForAdd', parameter).pipe(
            map((response: GroupProductPropertyAddModel) => response)
        );
    }

    public getEditMode(parameter: GroupProductPropertyParameterModel): Observable<GroupProductPropertyEditModel> {
        return this.http.post('admin/groupProductProperty/getForEdit', parameter).pipe(
            map((response: GroupProductPropertyEditModel) => response)
        );
    }

    public getListMode(parameter: GroupProductPropertyParameterModel): Observable<GroupProductPropertyGridModel> {
        return this.http.post('admin/groupProductProperty/getForList', parameter).pipe(
            map((response: GroupProductPropertyGridModel) => response)
        );
    }
}
