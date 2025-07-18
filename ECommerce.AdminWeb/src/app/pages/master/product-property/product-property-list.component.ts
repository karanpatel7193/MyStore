import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AccessModel } from 'src/app/models/access.model';
import { ToastService } from 'src/app/services/toast.service';
import { ProductPropertyParameterModel, ProductPropertyGridModel, ProductPropertyModel } from './product-property.model';
import { ProductService } from '../product/product.service';

@Component({
    selector: 'app-product-property-list',
    templateUrl: './product-property-list.component.html',
    styleUrls: ['./product-property-list.component.scss']
})
export class ProductPropertyListComponent implements OnInit {
    public productPropertyParameter: ProductPropertyParameterModel = new ProductPropertyParameterModel();
    public productPropertyGrid: ProductPropertyGridModel = new ProductPropertyGridModel();
    public productProperties: ProductPropertyModel[] = [];
    public productId: number = 0;
    public categoryId: number = 0;
    public productName: string = '';
    public access: AccessModel = new AccessModel();
    public hasAccess: boolean = false;
    public mode: string = '';
    private sub: any;


    constructor(private productService: ProductService,
        private toastr: ToastService,
        private router: Router,
        private route: ActivatedRoute,
    ) { }

    ngOnInit() {
        this.getRouteData();
    }

    public search(): void {
        this.productPropertyParameter.productId = this.productId;
        this.productPropertyParameter.categoryId = this.categoryId;
        this.productService.getForPropertyGrid(this.productPropertyParameter).subscribe(data => {
            this.productPropertyGrid = data;
        });
    }

    public getRouteData(): void {
        this.sub = this.route.params.subscribe(params => {
            this.productId = +params['productId'];
            this.categoryId = +params['categoryId'];
            this.productName = params['productName'];
            this.search();
        });
    }

    public submit(): void {
        this.productPropertyParameter.productId = this.productId;
        this.productPropertyParameter.productProperties = this.productPropertyGrid.productProperties;
        this.productService.insertProperty(this.productPropertyParameter).subscribe((data) => {
            this.toastr.success('Record submitted successfully.');
            this.search();
        });
    }

    public delete(id: number): void {
        if (!this.access.canDelete) {
            this.toastr.warning('You do not have delete access of this page.');
            return;
        }
        if (window.confirm('Are you sure you want to delete?')) {
            this.productService.delete(id).subscribe(data => {
                this.toastr.success('Record deleted successfully.');
                this.search();
            });
        }
    }

    public cancel(): void {
        this.router.navigate(['/app/master/product/list']);
    }
    
    public isDisabledProperty(propertyId: number): boolean {
        const variantIds = this.productPropertyGrid?.variantPropertyIds?.map(v => v.variantPropertyId);
        return variantIds?.includes(propertyId);
      }
      
}

