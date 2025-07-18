import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { SessionService } from '../../../services/session.service';
import { ToastService } from '../../../services/toast.service';
import { CategoryPropertyService } from './category-property.service';
import {  CategoryPropertyModel, CategoryPropertyParameterModel,} from './category-property.model';
import { AccessModel } from '../../../models/access.model';
import { PropertyMainModel } from '../property/property.model';

@Component({
  selector: 'app-category-property-form',
  templateUrl: './category-property-form.component.html',
})
export class CategoryPropertyFormComponent implements OnInit, OnDestroy {
  public access: AccessModel = new AccessModel();
  public categoryProperty: CategoryPropertyModel = new CategoryPropertyModel();
  public mode: string = '';
  public id: number = 0;
  public hasAccess: boolean = false;
  private sub: any;
    public categoryId: number = 0;
    public categoryName: string = '';

  public properties: PropertyMainModel[] = [];
  public categoryPropertyParameterModel: CategoryPropertyParameterModel = new CategoryPropertyParameterModel();
  constructor(
    private categoryPropertyService: CategoryPropertyService,
    private sessionService: SessionService,
    private router: Router,
    private route: ActivatedRoute,
    private toaster: ToastService
  ) {
    this.setAccess();
  }

  ngOnInit() {
    this.getRouteData();
  }

  ngOnDestroy() {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }

  public getRouteData(): void {
    this.sub = this.route.params.subscribe((params) => {
      const segments: UrlSegment[] = this.route.snapshot.url;
      this.categoryId = +params['categoryId']; 
      this.categoryName = params['categoryName'];  

      this.id = segments[0].toString().toLowerCase() === 'add' ? 0 : +params['id'];
      this.setPageMode();
    });
  }

  public setPageMode(): void {
    if (this.id === undefined || this.id === 0) {
      this.setPageAddMode();
    } else {
      this.setPageEditMode();
    }
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

    this.categoryPropertyService.getAddMode(this.categoryPropertyParameterModel).subscribe(data => {
      this.properties = data.properties;
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
    this.categoryPropertyParameterModel.categoryId = this.id;

    this.categoryPropertyService.getEditMode(this.categoryPropertyParameterModel).subscribe(data => {
        this.properties = data.properties;
        this.categoryProperty = data.categoryProperty;
    });
}
  public clearModels(): void {
    this.categoryProperty = new CategoryPropertyModel();
  }

  public save(isFormValid: boolean | null): void {
    if (!this.access.canInsert && !this.access.canUpdate) {
      this.toaster.warning('You do not have add or edit access to this page.');
      return;
    }

    if (isFormValid) {
      this.categoryProperty.categoryId = this.categoryId;
      this.categoryPropertyService.save(this.categoryProperty, this.mode).subscribe((data) => {
        if (data === 0) {
          this.toaster.warning('Record already exists.');
        } else if (data > 0) {
          this.toaster.success('Record submitted successfully.');
          this.router.navigate(['/app/master/category-property/list',this.categoryId, this.categoryName]);
          this.cancel();
        }
      });
    } else {
      this.toaster.warning('Please provide valid input.');
    }
  }

  public cancel(){
    this.router.navigate(['/app/master/category-property/list',this.categoryId, this.categoryName])
}

  public setAccess(): void {
    this.access = this.sessionService.getAccess('/app/master/category-property/list');
  }
}
