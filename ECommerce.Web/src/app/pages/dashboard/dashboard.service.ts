import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { HttpService } from '../../services/http.service';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class DashboardService {

    private apiUrl = environment.apiUrl;

    constructor(private http: HttpService) { }

    public getProfileData(): Observable<any> {
        return this.http.post(`${this.apiUrl}user/user-profile/`, {}).pipe(
            map((response: any) => response)
        );
    }

    public updateAvatar(data: any): Observable<any> {
        return this.http.post(`${this.apiUrl}user/profile_update_avatar/`, data).pipe(
            map((response: any) => response)
        );
    }
}
