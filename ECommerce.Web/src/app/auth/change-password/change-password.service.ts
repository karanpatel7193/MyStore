import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { UserModel } from '../../pages/account/user/user.model';
import { HttpService } from '../../services/http.service';

@Injectable({
    providedIn: 'root'
})
export class ChangePasswordService {
    constructor(private http: HttpService) { }

    public ChangePassword(userModel: UserModel): Observable<string> {
        return this.http.post('account/user/changepassword', userModel).pipe(
            map((response: any) => {
                return response;
            }),
        );
    }
}
