import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { SessionService } from '../../../services/session.service';
import { AccessModel } from '../../../models/access.model';
import { ToastService } from '../../../services/toast.service';
import { FileModel } from 'src/app/models/file.model';
import { VendorModel, VendorParameterModel } from './vendor.model';
import { VendorService } from './vendor.service';
import { CountryMainModel } from 'src/app/models/country.model';
import { StateMainModel, StateParameterModel } from 'src/app/pages/globalization/state/state.model';
import { StateService } from '../../globalization/state/state.service';

@Component({
	selector: 'app-vendor-add',
	templateUrl: './vendor-form.component.html',
	styleUrls: ['./vendor-form.component.scss']

})
export class VendorFormComponent implements OnInit, OnDestroy {
	public access: AccessModel = new AccessModel();
	public vendor: VendorModel = new VendorModel();
	public vendorParameter: VendorParameterModel = new VendorParameterModel();
	public stateParameterModel: StateParameterModel = new StateParameterModel();
	public hasAccess: boolean = false;
	public mode: string = '';
	public id: number = 0;
	private sub: any;
	public countries: CountryMainModel[] = [];
	public states: StateMainModel[] = [];
	public file: FileModel = new FileModel();

	constructor(
		private vendorService: VendorService,
		private stateService: StateService,
		private sessionService: SessionService,
		private router: Router,
		private route: ActivatedRoute,
		private toaster: ToastService,
	) {
		this.setAccess();
	}

	ngOnInit() {
		this.getRouteData();
	}

	ngOnDestroy() {
		this.sub.unsubscribe();
	}

	public onCountryChange(): void {
		this.stateParameterModel.countryId = this.vendor.countryId
		this.stateService.getForLOV(this.stateParameterModel).subscribe(data => {
			this.states = data; // Update states with the fetched data
		});
	  }
	
	

	public setAccess(): void {
		this.access = this.sessionService.getAccess('vendor/vendor-list');
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
		this.vendor = new VendorModel();
		this.vendor = new VendorModel();
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

		this.vendorService.getAddMode(this.vendorParameter).subscribe(data => {
			this.countries = data.countries;
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
		this.vendorParameter.id = this.id;

		this.vendorService.getEditMode(this.vendorParameter).subscribe(data => {
			this.vendor = data.vendor;
			this.countries = data.countries;
			if (this.vendor.countryId) {
				this.onCountryChange();
			}

		});
	}

	save(isFormValid: boolean | null): void {
		if (!this.access.canInsert && !this.access.canUpdate) {
			this.toaster.warning('You do not have add or edit access to this page.');
			return;
		}
		if (isFormValid) {
			this.vendorService.save(this.vendor, this.mode).subscribe((data) => {
				if (data === 0) {
					this.toaster.warning('Record already exists.');
				} else if (data > 0) {
					this.toaster.success('Record submitted successfully.');
					this.router.navigate(['app/vendor/vendor-list/list']);
					this.cancel();
				}
			});
		} else {
			this.toaster.warning('Please provide valid input.');
		}
	}

	public cancel(): void {
		this.router.navigate(['app/vendor/vendor-list/list']);
	}

	//other method
	
}
