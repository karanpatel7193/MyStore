import { Injectable } from '@angular/core';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { HttpService } from 'src/app/services/http.service';
import { BlockAddModel, BlockEditModel, BlockGridModel, BlockListModel, BlockMainModel, BlockModel, BlockParameterModel } from './block.model';

@Injectable()
export class BlockService {
    constructor(private http: HttpService) {
    }

    public getAddMode(blockParameter: BlockParameterModel): Observable<BlockAddModel> {
            return this.http.post('admin/block/getAddMode', blockParameter).pipe(
                map((response: BlockAddModel) => {
                    return response;
                }),
            );
        }
    
    public getEditMode(blockParameter: BlockParameterModel): Observable<BlockEditModel> {
        return this.http.post('admin/block/getEditMode', blockParameter).pipe(
            map((response: BlockEditModel) => {
                return response;
            }),
        );
    }
    public getRecord(id: number): Observable<BlockModel> {
        return this.http.get('admin/block/getRecord/' + id).pipe(
            map((response: BlockModel) => {
                return response;
            }),
        );
    }

    public getForLOV(blockParameter: BlockParameterModel): Observable<BlockMainModel[]> {
            return this.http.post('admin/block/getLovValue', blockParameter).pipe(
                map((response: BlockMainModel[]) => {
                    return response;
                }),
            );
    }

    public getForGrid(blockParameter: BlockParameterModel): Observable<BlockGridModel> {
        return this.http.post('admin/block/getForGrid', blockParameter).pipe(
            map((response: BlockGridModel) => {
                return response;
            }),
        );
    }

    public getListMode(blockParameter: BlockParameterModel): Observable<BlockListModel> {
        return this.http.post('admin/block/getListValue', blockParameter).pipe(
            map((response: BlockListModel) => {
                return response;
            }),
        );
    }

    public save(block: BlockModel, mode: string): Observable<number> {
        if (mode == 'Add')
            return this.http.post('admin/block/insert', block).pipe(
                map((response: number) => {
                    return response;
                }),
            );
        else
            return this.http.post('admin/block/update', block).pipe(
                map((response: number) => {
                    return response;
                }),
            );
    }

    public delete(id: number): Observable<void> {
        return this.http.post('admin/block/delete/' + id, null).pipe(
            map((response: void) => {
                return response;
            }),
        );
    }

}
