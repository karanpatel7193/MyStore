import { Component, OnDestroy, OnInit } from '@angular/core';
import { PropertyModel, PropertyParameterModel } from '../property.model';
import { Router, ActivatedRoute, UrlSegment } from '@angular/router';
import { AccessModel } from 'src/app/models/access.model';
import { SessionService } from 'src/app/services/session.service';
import { ToastService } from 'src/app/services/toast.service';
import { PropertyService } from '../property.service';

@Component({
  selector: 'app-property-form',
  templateUrl: './property-form.component.html',
  styleUrls: ['./property-form.component.scss']
})
export class PropertyFormComponent implements OnInit , OnDestroy {
        public access: AccessModel = new AccessModel();
        public property: PropertyModel = new PropertyModel();
        public properties: PropertyModel[] = [];
        public propertyParameter: PropertyParameterModel = new PropertyParameterModel();
    
        public hasAccess: boolean = false;
        public mode: string ='';
        public id: number=0;
        private sub: any;
    
        constructor(private propertyService: PropertyService,
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
            this.property = new PropertyModel();
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
    
            this.clearModels();
        }
    
        public setPageEditMode(): void {
            if (!this.access.canUpdate) {
                this.toaster.warning('You do not have update access of this page.');
                return;
            }
            this.hasAccess = true;
            this.mode = 'Edit';
    
            this.propertyService.getRecord(this.id).subscribe(data => {
                this.property = data;
            });
        }
    
        public add(): void {
            if (!this.access.canInsert) {
                this.toaster.warning('You do not have add access of this page.');
                return;
            }
    
            this.router.navigate(['app/master/property/list']);
        }
    
        public edit(id: number): void {
            if (!this.access.canUpdate) {
                this.toaster.warning('You do not have edit access of this page.');
                return;
            }
    
            this.router.navigate(['app/master/property/edit', id]);
        }
    
        public save(isFormValid: boolean): void {
            if (!this.access.canInsert && !this.access.canUpdate) {
                this.toaster.warning('You do not have add or edit access of this page.');
                return;
            }
            if (isFormValid) {
                this.propertyService.save(this.property).subscribe(data => {
                    if (data === 0)
                        this.toaster.warning('Record is already exist.');
                    else if (data > 0) {
                        this.toaster.success('Record submitted successfully.');
                        this.router.navigate(['app/master/property/list']);
                    }
                });
            } else {
                this.toaster.warning('Please provide valid input.');
            }
        }
    
        public cancle():void{
            this.router.navigate(['app/master/property/list'])
        }
        public setAccess(): void {
            this.access = this.sessionService.getAccess('master/property');
       }
}