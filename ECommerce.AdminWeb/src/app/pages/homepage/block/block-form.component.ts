import { Component, OnInit} from '@angular/core';
import { Router, ActivatedRoute, UrlSegment } from '@angular/router';
import { AccessModel } from 'src/app/models/access.model';
import { SessionService } from 'src/app/services/session.service';
import { ToastService } from 'src/app/services/toast.service';
import { ProductMainModel, ProductModel, ProductParameterModel } from '../../master/product/product.model';
import { BlockParameterModel, BlockGridModel, BlockModel, BlockAddModel, BlockEditModel, BlockProductModel } from './block.model';
import { BlockService } from './block.service';

@Component({
  selector: 'app-block',
  templateUrl: './block-form.component.html',
  styleUrls: ['./block-form.component.scss']
})
export class BlockFormComponent implements OnInit {

    public access: AccessModel = new AccessModel();
    public blockModel: BlockModel = new BlockModel();
    public blockParameter: BlockParameterModel = new BlockParameterModel();
    public blockGridModel: BlockGridModel = new BlockGridModel();
    public blockAdd: BlockAddModel = new BlockAddModel();
    public blockEdit: BlockEditModel = new BlockEditModel();
    public hasAccess: boolean = false;
    public id: number = 0;
    public mode: string = '';
    private sub: any;
    public products: ProductMainModel[] = [];
    public product: ProductMainModel = new ProductMainModel();
    
    constructor(private blockService: BlockService,
        private sessionService: SessionService,
        private route: ActivatedRoute,
        private router: Router,
        private toaster: ToastService) {
        this.setAccess();
    }

    ngOnInit() {
        this.getRouteData()
    }

    ngOnDestroy(): void {
      }

    public getRouteData(): void {
        this.sub = this.route.params.subscribe(params => {
            const segments: UrlSegment[] = this.route.snapshot.url;
            if (segments.toString().toLowerCase() === 'add')
                this.id = 0;
            else
                this.id = +params['id']; // (+) converts string 'id' to a number
            this.setPageMode();
        });
    }

    public setPageMode(): void {
        if (this.id === undefined || this.id === 0)
            this.setPageAddMode();
        else
            this.setPageEditMode();

        if (this.hasAccess) {
        }
    }

    public setPageAddMode(): void {
        if (!this.access.canInsert) {
            this.toaster.warning('You do not have add access of this page.');
            return;
        }
        this.hasAccess = true;
        this.mode = 'Add';
        this.blockService.getAddMode(this.blockParameter).subscribe(data => {
            this.blockAdd = data;
            //this.products = this.blockAdd.products;
        });
        this.clearModels();
    }

    public setPageEditMode(): void {
        if (!this.access.canUpdate) {
            this.toaster.warning('You do not have update access of this page.');
            return;
        }
        this.hasAccess = true;
        this.mode = 'Edit';
        this.blockParameter.id = this.id;

        this.blockService.getEditMode(this.blockParameter).subscribe(data => {
            this.blockEdit = data;
            this.blockModel = this.blockEdit.block;
            this.blockModel.blockProducts = this.blockEdit.block.blockProducts;
            //this.products = this.blockEdit.products;
        });
    }

    public save(isFormValid: boolean): void {
        if (!this.access.canInsert && !this.access.canUpdate) {
            this.toaster.warning('You do not have add or edit access of this page.');
            return;
        }

        if (isFormValid) {
            debugger
            this.blockService.save(this.blockModel, this.mode).subscribe(data => {
                if (data === 0){
                    this.toaster.warning('Record is already exist.');
                }    
                else if (data > 0) {
                    this.toaster.success('Record submitted successfully.');
                    this.router.navigate(['app/homepage/block/list']);
                }
            });
        } else {
            this.toaster.warning('Please provide valid input.');
        }
    }

    public onProductSelected(product: ProductMainModel, productSelector: any): void {
        if (this.blockModel.blockProducts.filter(x => x.productId === product.id).length == 0) {
            this.blockModel.blockProducts.push({
                productId: product.id,
                id: 0,
                blockId: this.mode === 'Edit' ? this.id : 0,
                productName: product.name
            });
        }
        productSelector.reset();
    }

    public removeProduct(index: number): void {
        this.blockModel.blockProducts.splice(index, 1); 
    }

    public cancel():void{
        this.router.navigate(['app/homepage/block/list']);
    }

    public clearModels(): void {
        this.blockModel = new BlockModel();
        this.product = new ProductMainModel();
    }

    public setAccess(): void {
        this.access = this.sessionService.getAccess('homepage/block');
    }
}
