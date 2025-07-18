import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { SessionService } from '../../../services/session.service';
import { ToastService } from '../../../services/toast.service';
import { AccessModel } from '../../../models/access.model';
import { PropertyMainModel } from '../property/property.model';
import { GroupProductPropertyService } from './group-product-property.service';
import { GroupProductPropertyModel, GroupProductPropertyParameterModel } from './group-product-property.model';

@Component({
  selector: 'app-group-product-property-form',
  templateUrl: './group-product-property-form.component.html',
})
export class GroupProductPropertyFormComponent implements OnInit, OnDestroy {
  public access: AccessModel = new AccessModel();
  public groupProductProperty: GroupProductPropertyModel = new GroupProductPropertyModel(); // Correct property name
  public mode: string = '';
  public id: number = 0;
  public hasAccess: boolean = false;
  private sub: any;
  public groupProductId: number = 0;
  public properties: PropertyMainModel[] = [];
  public groupProductPropertyParameterModel: GroupProductPropertyParameterModel = new GroupProductPropertyParameterModel();

  constructor(
    private groupProductPropertyService: GroupProductPropertyService,
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
      this.groupProductId = +params['groupProductId'];  
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
    if (this.hasAccess) {}
  }

  public setPageAddMode(): void {
    if (!this.access.canInsert) {
        this.toaster.warning('You do not have add access of this page.');
        return;
    }
    this.hasAccess = true;
    this.mode = 'Add';

    this.groupProductPropertyService.getAddMode(this.groupProductPropertyParameterModel).subscribe(data => {
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
    this.groupProductPropertyParameterModel.groupProductId = this.id;

    this.groupProductPropertyService.getEditMode(this.groupProductPropertyParameterModel).subscribe(data => {
        this.properties = data.properties;
        this.groupProductProperty = data.groupProductProperty;
    });
  }

  public clearModels(): void {
    this.groupProductProperty = new GroupProductPropertyModel();
  }

  public save(isFormValid: boolean | null): void {
    if (!this.access.canInsert && !this.access.canUpdate) {
      this.toaster.warning('You do not have add or edit access to this page.');
      return;
    }

    if (isFormValid) {
      this.groupProductProperty.groupProductId = this.groupProductId;
      this.groupProductPropertyService.save(this.groupProductProperty, this.mode).subscribe((data) => {
        if (data === 0) {
          this.toaster.warning('Record already exists.');
        } else if (data > 0) {
          this.toaster.success('Record submitted successfully.');
          this.router.navigate(['app/category-property/list']);
          this.cancel();
        }
      });
    } else {
      this.toaster.warning('Please provide valid input.');
    }
  }

  public cancel() {
    this.router.navigate(['/app/master/group-product-property/list', this.groupProductId]);
  }

  public setAccess(): void {
    this.access = this.sessionService.getAccess('/app/master/group-product-property/list');
  }
}
