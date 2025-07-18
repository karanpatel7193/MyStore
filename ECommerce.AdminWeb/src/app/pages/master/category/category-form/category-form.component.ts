import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, ActivatedRoute, UrlSegment } from '@angular/router';
import { AccessModel } from 'src/app/models/access.model';
import { SessionService } from 'src/app/services/session.service';
import { ToastService } from 'src/app/services/toast.service';
import { CategoryService } from '../category.service';
import { CategoryModel, CategoryMainModel, CategoryAddModel, CategoryEditModel, CategoryParameterModel} from '../category.model';
import { FileModel } from 'src/app/models/file.model';

@Component({
  selector: 'app-add-category',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.scss']
})
export class CategoryFormComponent implements OnInit, OnDestroy {
    public access: AccessModel = new AccessModel();

    public categoryModel: CategoryModel = new CategoryModel();
    public parentCategories: CategoryMainModel[] = [];
    public categories: CategoryModel[] = [];
    public categoryAdd: CategoryAddModel = new CategoryAddModel();
    public categoryEdit: CategoryEditModel = new CategoryEditModel();
    public categoryParameter: CategoryParameterModel = new CategoryParameterModel();
    public file: FileModel = new FileModel();

    public hasAccess: boolean = false;
    public mode: string ='';
    public id: number=0;
    private sub: any;

    constructor(private categoryService: CategoryService,
        private sessionService: SessionService,
        private router: Router,
        private route: ActivatedRoute,
        private toaster: ToastService) {
            this.setAccess();
    }

    ngOnInit() {
        this.getRouteData();
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
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

    public clearModels(): void {
        this.categoryModel = new CategoryModel();
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

       this.categoryService.getAddMode(this.categoryParameter).subscribe((data: CategoryAddModel) => {
            this.categoryAdd = data;
            this.parentCategories = this.categoryAdd.parentCategories;
        });
        this.clearModels();
    }

    public setPageEditMode(): void {
        if (!this.access.canUpdate) {
            this.toaster.warning('You do not have update access for this page.');
            return;
        }
        this.hasAccess = true;
        this.mode = 'Edit';
    
        this.categoryParameter.id = this.id;

        this.categoryService.getEditMode(this.categoryParameter).subscribe((data) => {
            this.categoryEdit = data;
            this.categoryModel = this.categoryEdit.category;
            this.parentCategories = this.categoryEdit.parentCategories;
        }); 
    }
    
    public fileUpload(files: FileModel): void {
        this.categoryModel.file = files;
    }

    public add(): void {
        if (!this.access.canInsert) {
            this.toaster.warning('You do not have add access of this page.');
            return;
        }

        this.router.navigate(['app/master/category/list']);
    }

    public edit(id: number): void {
        if (!this.access.canUpdate) {
            this.toaster.warning('You do not have edit access of this page.');
            return;
        }

        this.router.navigate(['app/master/category/edit', id]);
    }

    public save(isFormValid: boolean): void {
        if (isFormValid) {
            if (!this.access.canInsert && !this.access.canUpdate) {
                this.toaster.warning('You do not have add or edit access of this page.');
                return;
            }

            this.categoryService.save(this.categoryModel).subscribe(data => {
                if (data === 0)
                    this.toaster.warning('Record is already exist.');
                else if (data > 0) {
					this.toaster.success('Record submitted successfully.');
                    this.router.navigate(['app/master/category/list']);
                }
            });
        } else {
			this.toaster.warning('Please provide valid input.');
        }
    }

    public cancel():void{
        this.router.navigate(['app/master/category/list'])
    }
    public setAccess(): void {
        this.access = this.sessionService.getAccess('master/category');
    }
}
