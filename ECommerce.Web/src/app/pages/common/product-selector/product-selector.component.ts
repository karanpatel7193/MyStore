import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Observable, debounceTime, distinctUntilChanged, filter, map } from 'rxjs';
import { ProductMainModel, ProductParameterModel } from '../../master/product/product.model';
import { ProductService } from '../../master/product/product.service';
import { Router, ActivatedRoute } from '@angular/router';
import { SessionService } from 'src/app/services/session.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-product-selector',
  templateUrl: './product-selector.component.html',
  styleUrls: ['./product-selector.component.scss']
})
export class ProductSelectorComponent implements OnInit {
    public products: ProductMainModel[] = [];
    public product: ProductMainModel = new ProductMainModel();
    public selectedProduct: ProductMainModel = new ProductMainModel();
    public productParameterModel: ProductParameterModel = new ProductParameterModel();

    @Output() productSelected = new EventEmitter<ProductMainModel>();

    formatter = (product: ProductMainModel) => product.name;
    search = (text$: Observable<string>) =>
        text$.pipe(
            debounceTime(200),
            distinctUntilChanged(),
            filter((term) => term.length >= 2),
            map((term) => this.products.filter((product) => new RegExp(term, 'mi').test(product.name)).slice(0, 10)),
        );

    constructor(private productService: ProductService) { }

    ngOnInit(): void {
		this.fillDropdowns();
    }

    private fillDropdowns(): void {
		this.productService.getForLOV(this.productParameterModel).subscribe(
			(data) => {
				this.products = data;
			},
			error => {
				console.error('Failed to load roles:', error);
			}
		);
	}

    public onProductSelected(event: any): void {
        this.productSelected.emit(this.selectedProduct);
    }

    public reset(): void {
        this.selectedProduct =  new ProductMainModel();
    }
}
  