import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { PageModel } from 'src/app/models/page.model';
import { UserLoginModel } from 'src/app/pages/account/user/user.model';
import { UserService } from 'src/app/pages/account/user/user.service';
import { ScriptService } from 'src/app/pages/master/script/script.service';
import { CommonService } from 'src/app/services/common.service';
import { SessionService } from 'src/app/services/session.service';
import { environment } from 'src/environments/environment';


@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    public commonPageModel: PageModel = new PageModel();
    public currentUser: UserLoginModel = new UserLoginModel();
    showProfileModal: boolean = false;
    public swVersion: string = environment.softwareVersion;
    public modelVersion: string = '';


    constructor(private router: Router, private activatedRoute: ActivatedRoute, private commonService: CommonService,
        private sessionService: SessionService, private userService: UserService, private scriptService: ScriptService) {
        router.events.subscribe((val) => {
            if (val instanceof NavigationEnd) {
            }
        });
    }

    ngOnInit(): void {
        this.currentUser = this.sessionService.getUser();
        this.sessionService.userState.subscribe(data => {
            this.currentUser = this.sessionService.getUser();
        });
    }


    public logout() {
        this.sessionService.logout()
    }

    public cancel(): void {
        this.router.navigate(['auth/login'])
    }
    openProfileModal() {
        this.showProfileModal = true;
    }

    closeProfileModal() {
        this.showProfileModal = false;
    }

    public onScriptSelected(event: any) {
        if (event) {
            this.router.navigate([`app/scriptView/${event.item.id}`]);
        }
    }

    scrollLeft() {
        const container = document.querySelector('.ttape-inner');
        if (container) {
            container.scrollBy({ left: -100, behavior: 'smooth' });
        }
    }

    scrollRight() {
        const container = document.querySelector('.ttape-inner');
        if (container) {
            container.scrollBy({ left: 100, behavior: 'smooth' });
        }
    }
    public redirect(id: number, symbol: string) {
        this.commonService.redirectToPage(id, symbol);
    }

    //code for password change
    showModal: boolean = false;

    goToChangePassword() {
        this.showModal = true;
    }

    closeModal() {
        this.showModal = false;
    }
    onBackdropClick(event: MouseEvent) {
        const modalContent = document.querySelector('.modal-content');
        if (modalContent && !modalContent.contains(event.target as Node)) {
            this.closeModal();
            this.closeProfileModal();
        }
    }

    stopPropagation(event: MouseEvent) {
        event.stopPropagation();
    }
}


