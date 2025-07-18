import { Component, OnInit } from '@angular/core';
import { CategoryPropertyService } from './category-property.service';
import { CategoryPropertyParameterModel, CategoryPropertyGridModel, CategoryPropertyModel, categoryPropertyMainModel } from './category-property.model';
import { AccessModel } from 'src/app/models/access.model';
import { ToastService } from 'src/app/services/toast.service';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';

@Component({
    selector: 'category-property-list',
    templateUrl: './category-property-list.component.html',
    styleUrls: []
})
export class CategoryPropertyListComponent implements OnInit {
    public categoryPropertyParameter: CategoryPropertyParameterModel = new CategoryPropertyParameterModel();
    public categoryPropertyGrid: CategoryPropertyGridModel = new CategoryPropertyGridModel();
    public categoryProperty: CategoryPropertyModel []= [];
	public categoryId: number = 0;
	public categoryName: string = '';
    public access: AccessModel = new AccessModel();
    public hasAccess: boolean = false;
    public mode: string = '';
    private sub: any;


    constructor(private categoryPropertyService: CategoryPropertyService,
        private toastr: ToastService,
        private router: Router,
        private route: ActivatedRoute,
    ) {}

    ngOnInit() {
        this.getRouteData();
    }

    public search(): void {
        this.categoryPropertyParameter.categoryId = this.categoryId;
        this.categoryPropertyService.getForGrid(this.categoryPropertyParameter).subscribe(data => {
            this.categoryPropertyGrid = data;
        });
    }

    public getRouteData(): void {
        this.sub = this.route.params.subscribe(params => {
            this.categoryId = +params['categoryId'];  
            this.categoryName = params['categoryName'];  
            this.search(); 
        });
    }
    
    public add(): void {
		if (!this.access.canInsert) {
			this.toastr.warning('You do not have add access of this page.');
			return;
		}

		this.router.navigate(['/app/master/category-property/add', this.categoryId, this.categoryName]);
	}
    

    public sort(sortExpression: string): void {
        if (this.categoryPropertyParameter.sortExpression === sortExpression) {
            this.categoryPropertyParameter.sortDirection = this.categoryPropertyParameter.sortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            this.categoryPropertyParameter.sortExpression = sortExpression;
            this.categoryPropertyParameter.sortDirection = 'asc';
        }
        this.search();
    }

    public edit(id: number): void {
		if (!this.access.canUpdate) {
			this.toastr.warning('You do not have edit access of this page.');
			return;
		}

		this.router.navigate(['/app/master/category-property/edit', this.categoryId, this.categoryName, id]);
	}

	public delete(id: number): void {
		if (!this.access.canDelete) {
			this.toastr.warning('You do not have delete access of this page.');
			return;
		}
		if (window.confirm('Are you sure you want to delete?')) {
			this.categoryPropertyService.delete(id).subscribe(data => {
				this.toastr.success('Record deleted successfully.');
				this.search();
			});
		}	
	}

    public cancel(): void {
        this.router.navigate(['/app/master/category/list']);
    }
}
