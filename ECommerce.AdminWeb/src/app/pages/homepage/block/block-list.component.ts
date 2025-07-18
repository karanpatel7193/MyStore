import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { AccessModel } from 'src/app/models/access.model';
import { SessionService } from 'src/app/services/session.service';
import { ToastService } from 'src/app/services/toast.service';
import { ProductMainModel } from '../../master/product/product.model';
import { BlockModel, BlockParameterModel, BlockGridModel, BlockAddModel, BlockEditModel } from './block.model';
import { BlockService } from './block.service';

@Component({
  selector: 'app-block-list',
  templateUrl: './block-list.component.html',
  styleUrls: ['./block-list.component.scss']
})
export class BlockListComponent implements OnInit {
    public access: AccessModel = new AccessModel();
    public blockModel: BlockModel = new BlockModel();
    public blockParameter: BlockParameterModel = new BlockParameterModel();
    public blockGridModel: BlockGridModel = new BlockGridModel();
    public hasAccess: boolean = false;
    public id: number = 0;
    public mode: string = '';
    private sub: any;

    constructor(private blockService: BlockService,
        private sessionService: SessionService,
        private route: ActivatedRoute,
        private router: Router,
        private toaster: ToastService) {
        this.setAccess();
    }

    ngOnInit() {
        this.setPageListMode();
    }

    public reset(): void {
        this.blockParameter = new BlockParameterModel();
        this.blockParameter.sortExpression = 'Id';
        this.blockParameter.sortDirection = 'asc';
        this.search();
    }

    public search(): void {
        if (!this.access.canView) {
            this.toaster.warning('You do not have view access to this page.');
            return;
        }
        this.blockService.getListMode(this.blockParameter).subscribe(data => {
            this.blockGridModel = data;
        });
    }

    public sort(sortExpression: string): void {
        if (sortExpression === this.blockParameter.sortExpression) {
            this.blockParameter.sortDirection = this.blockParameter.sortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            this.blockParameter.sortExpression = sortExpression;
            this.blockParameter.sortDirection = 'asc';
        }
        this.search();
    }

    public add(): void {
        if (!this.access.canInsert) {
            this.toaster.warning('You do not have add access to this page.');
            return;
        }
        this.router.navigate(['app/homepage/block/add']);
        this.search();
    }

    public edit(id: number): void {
        if (!this.access.canUpdate) {
            this.toaster.warning('You do not have edit access to this page.');
            return;
        }
        this.router.navigate(['app/homepage/block/edit', id]);
    }

    public delete(id: number): void {
        if (!this.access.canDelete) {
            this.toaster.warning('You do not have delete access to this page.');
            return;
        }

        if (window.confirm('Are you sure you want to delete?')) {
            this.blockService.delete(id).subscribe(data => {
                this.toaster.success('Record deleted successfully.');
                this.search();
            });
        }
    }

    public setPageListMode(): void {
        if (!this.access.canView) {
            this.toaster.warning('You do not have view access to this page.');
            return;
        }

        this.blockParameter.sortExpression = 'Id';
        this.search();
    }

    public setAccess(): void {
        this.access = this.sessionService.getAccess('homepage/block');
    }
}
