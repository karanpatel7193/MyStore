import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BlockGridModel } from './block.model';
import { environment } from '../../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class BlockService {
    private apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    public getBlockList(): Observable<BlockGridModel> {
        return this.http.post<BlockGridModel>(`${this.apiUrl}home/getBlockList`, {});
    }
    public getCategoryList(): Observable<any> {
        return this.http.post<any>(`${this.apiUrl}home/getCategoryList`, {});
    }

    // In blockService

getProductsByBlockId(blockId: number) {
    return this.http.get(`/api/products/block/${blockId}`);  // Adjust API endpoint as necessary
  }
  
}
