import { Component, OnInit } from '@angular/core';
import { GroupProductPropertyService } from './group-product-property.service';
import { AccessModel } from 'src/app/models/access.model';
import { ToastService } from 'src/app/services/toast.service';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupProductPropertyGridModel, GroupProductPropertyModel, GroupProductPropertyParameterModel } from './group-product-property.model';

@Component({
    selector: 'app-group-product-property-list',
    templateUrl: './group-product-property-list.component.html',
    styleUrls: []
})
export class GroupProductPropertyListComponent implements OnInit {
    public groupProductPropertyParameterModel: GroupProductPropertyParameterModel = new GroupProductPropertyParameterModel();
    public groupProductPropertyGrid: GroupProductPropertyGridModel = new GroupProductPropertyGridModel();
    public groupProductId: number = 0;
    public categoryId: number = 0;
    public access: AccessModel = new AccessModel();
    public hasAccess: boolean = false;
    private sub: any;

    constructor(private groupProductPropertyService: GroupProductPropertyService,
                private toastr: ToastService,
                private router: Router,
                private route: ActivatedRoute) {}

    ngOnInit() {
        this.getRouteData();
    }

    public search(): void {
        this.groupProductPropertyParameterModel.groupProductId = this.groupProductId;
        this.groupProductPropertyService.getForGrid(this.groupProductPropertyParameterModel).subscribe(data => {
            this.groupProductPropertyGrid = data;
        });
    }

    public getRouteData(): void {
        this.sub = this.route.params.subscribe(params => {
            this.groupProductId = +params['groupProductId'];  
            this.search(); 
        });
    }

    public add(): void {
        if (!this.access.canInsert) {
            this.toastr.warning('You do not have add access to this page.');
            return;
        }

        this.router.navigate(['/app/master/group-product-property/add', this.groupProductId]);
    }

    public sort(sortExpression: string): void {
        if (this.groupProductPropertyParameterModel.sortExpression === sortExpression) {
            this.groupProductPropertyParameterModel.sortDirection = this.groupProductPropertyParameterModel.sortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            this.groupProductPropertyParameterModel.sortExpression = sortExpression;
            this.groupProductPropertyParameterModel.sortDirection = 'asc';
        }
        this.search();
    }

    public edit(id: number): void {
        if (!this.access.canUpdate) {
            this.toastr.warning('You do not have edit access to this page.');
            return;
        }

        this.router.navigate(['/app/master/group-product-property/edit', this.groupProductId, id]);
    }

    public delete(id: number): void {
        if (!this.access.canDelete) {
            this.toastr.warning('You do not have delete access to this page.');
            return;
        }
        if (window.confirm('Are you sure you want to delete?')) {
            this.groupProductPropertyService.delete(id).subscribe(() => {
                this.toastr.success('Record deleted successfully.');
                this.search();
            });
        }    
    }

    public cancel(): void {
        this.router.navigate(['/app/master/group-product/list']);
    }
}
