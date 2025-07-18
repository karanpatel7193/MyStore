import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { HttpService } from '../../../services/http.service';
import {
    UserModel,
    UserMainModel,
    UserParameterModel,
    UserGridModel,
    UserLoginModel,
    UserUpdateModel
} from './user.model';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private apiUrl = environment.apiImageUrl;

    constructor(private http: HttpService) { }

    public getRecord(id: number): Observable<UserModel> {
        return this.http.get(`${this.apiUrl}/account/user/getRecord/${id}`).pipe(
            map((response: any) => response)
        );
    }

    public getForLOV(params: UserParameterModel): Observable<UserMainModel[]> {
        return this.http.post(`${this.apiUrl}/account/user/getLovValue`, params).pipe(
            map((response: any) => response)
        );
    }

    public getForGrid(params: UserParameterModel): Observable<UserGridModel> {
        return this.http.post(`${this.apiUrl}/account/user/getGridData`, params).pipe(
            map((response: any) => response)
        );
    }

    public save(user: UserModel, mode: string): Observable<number> {
        const endpoint = mode === 'Add' ? 'insert' : 'update';
        return this.http.post(`${this.apiUrl}/account/user/${endpoint}`, user).pipe(
            map((response: any) => response)
        );
    }

    public delete(id: number): Observable<void> {
        return this.http.post(`${this.apiUrl}/account/user/delete/${id}`, {}).pipe(
            map((response: any) => response)
        );
    }

    public registration(user: UserModel): Observable<number> {
        return this.http.post(`${this.apiUrl}/account/user/registration`, user).pipe(
            map((response: any) => response)
        );
    }

    public registrationActive(activation: string): Observable<string> {
        return this.http.get(`${this.apiUrl}/account/user/registrationActive?Activation=${activation}`).pipe(
            map((response: any) => response)
        );
    }

    public regenerateRegistrationActive(activation: string): Observable<string> {
        return this.http.get(`${this.apiUrl}/account/user/regenerateRegistrationActive?Activation=${activation}`).pipe(
            map((response: any) => response)
        );
    }

    public userUpdate(model: UserUpdateModel): Observable<number> {
        return this.http.post(`${this.apiUrl}/account/user/userUpdate`, model).pipe(
            map((response: any) => response)
        );
    }

    public validateLogin(user: UserModel): Observable<UserLoginModel> {
        return this.http.post(`${this.apiUrl}/account/user/validateLogin`, user).pipe(
            map((response: any) => response)
        );
    }

    public resetPassword(user: UserModel): Observable<boolean> {
        return this.http.post(`${this.apiUrl}/account/user/resetPassword`, user).pipe(
            map((response: any) => response)
        );
    }

    public updatePassword(user: UserModel): Observable<string> {
        return this.http.post(`${this.apiUrl}/account/user/updatePassword`, user).pipe(
            map((response: any) => response)
        );
    }
}




// 'https://localhost:7143//account/user/getRecord';