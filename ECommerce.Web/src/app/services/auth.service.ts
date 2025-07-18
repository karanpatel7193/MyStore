import { Injectable } from '@angular/core';
import { SessionService } from './session.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
    constructor(private session: SessionService) { }

    public IsAuthenticated(): boolean {
        const user = this.session.getUser();
        return (user != null && user.token != null);
    }
}
