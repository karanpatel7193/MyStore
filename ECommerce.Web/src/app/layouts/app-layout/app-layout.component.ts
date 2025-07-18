import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from '../../services/common.service';
import { SessionService } from '../../services/session.service';

@Component({
    selector: 'app-app-layout',
    templateUrl: './app-layout.component.html',
    styleUrls: ['./app-layout.component.scss']
})
export class AppLayoutComponent implements OnInit {
    public pageName: string = 'test';

    constructor(private activatedRoute: ActivatedRoute, public sessionService: SessionService, private commonService: CommonService) {
        activatedRoute.url.subscribe(() => {
            this.pageName = activatedRoute.snapshot.firstChild?.data['pageName'] || '';
        });
        
    }

    ngOnInit(): void {
        
    }

}
