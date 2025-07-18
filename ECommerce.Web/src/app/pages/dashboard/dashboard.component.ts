import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { SessionService } from "../../services/session.service";
import { ToastService } from "../../services/toast.service";
import { DashboardService } from "./dashboard.service";


@Component({
    selector: 'app-dashboard-page',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
    public isLoading: boolean = false;

    constructor(private sessionService: SessionService, private DashboardService: DashboardService, private toastService: ToastService ,private route: ActivatedRoute) { }

    ngOnInit(): void {
    }

}
