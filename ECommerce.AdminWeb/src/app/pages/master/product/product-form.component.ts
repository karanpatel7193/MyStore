import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { SessionService } from '../../../services/session.service';
import { AccessModel } from '../../../models/access.model';
import { ToastService } from '../../../services/toast.service';
import { ProductMediaModel, ProductModel, ProductParameterModel } from './product.model';
import { ProductService } from './product.service';
import { MasterValuesModel } from 'src/app/models/mastervalue.model';
import { CategoryMainModel } from '../category/category.model';
import { FileModel } from 'src/app/models/file.model';
import { PropertyMainModel } from '../property/property.model';
import { CategoryPropertyService } from '../category-property/category-property.service';
import { CategoryPropertyModel, CategoryPropertyParameterModel } from '../category-property/category-property.model';
import { ProductVariantModel, ProductVariantParameterModel } from '../productVariant/productVariant.model';

@Component({
	selector: 'app-product-add',
	templateUrl: './product-form.component.html',
	styleUrls: ['./product-form.component.scss']

})
export class ProductFormComponent implements OnInit, OnDestroy {
	public access: AccessModel = new AccessModel();
	public product: ProductModel = new ProductModel();
	public productParameter: ProductParameterModel = new ProductParameterModel();
	public productVariantParameterModel: ProductVariantParameterModel = new ProductVariantParameterModel();
	public variantCombinationss: ProductVariantModel = new ProductVariantModel();
	public productParameterModel: ProductParameterModel = new ProductParameterModel();

	public hasAccess: boolean = false;
	public hideVariantList: boolean = false;
	public hideCombinationForm: boolean = false;
	public mode: string = '';
	public id: number = 0;
	private sub: any;
	public masterValues: MasterValuesModel[] = [];
	public categories: CategoryMainModel[] = [];
	public variantCombinations: ProductVariantModel[] = [];
	public variants: ProductModel[] = [];
	public productMediaModel: ProductMediaModel = new ProductMediaModel();
	public file: FileModel = new FileModel();
	public categoryPropertyParameterModel: CategoryPropertyParameterModel = new CategoryPropertyParameterModel();
	public categoryPropertyModel: CategoryPropertyModel[] = [];
	public categoryProperty: CategoryPropertyModel = new CategoryPropertyModel();

	constructor(
		private productService: ProductService,
		private sessionService: SessionService,
		private router: Router,
		private route: ActivatedRoute,
		private toaster: ToastService,
		private categoryPropertyService: CategoryPropertyService,
	) {
		this.setAccess();
	}

	ngOnInit() {
		this.getRouteData();
		this.loadMasterValues();
	}
	loadMasterValues() {
		this.masterValues = this.sessionService.getUser().masterValues.filter(x => x.masterId == 4);
	}
	ngOnDestroy() {
		this.sub.unsubscribe();
	}

	public setAccess(): void {
		this.access = this.sessionService.getAccess('master/product');
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
		this.product = new ProductModel();
		this.product = new ProductModel();
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

		this.productService.getAddMode(this.productParameter).subscribe(data => {
			this.categories = data.categories;

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
		this.productParameter.id = this.id;

		this.productService.getEditMode(this.productParameter).subscribe(data => {
			this.product = data.product;
			this.categories = data.categories;
			this.variantCombinations = data.variantCombinations.map((item, i) => ({
				...item,
				index: i + 1
			}));

			this.useGeneratedMatrix = false;

			this.variants = data.variants;
			this.apiMatrix = data.variants;
			this.product.variants.forEach((variant: any) => {
				if (Array.isArray(variant.productApiVariantIds)) {
					variant.productVariantIds = [...variant.productApiVariantIds].join(',');
				} else {
					variant.productVariantIds = variant.productApiVariantIds || '';
				}
			});



			this.productVariantParameterModel.productId = this.product.id;
			if (this.product?.categoryId) {
				this.loadPropertiesByCategory(this.product.categoryId);
			}
			if (this.product?.parentProductId > 0) {
				this.hideVariantList = true;
				this.hideCombinationForm = true;
			} else {
				this.hideVariantList = false;
				this.hideCombinationForm = false;
			}
			// this.refreshMatrix();

		});
	}

	save(isFormValid: boolean | null): void {
		if (!this.access.canInsert && !this.access.canUpdate) {
			this.toaster.warning('You do not have add or edit access to this page.');
			return;
		}

		if (isFormValid) {

			this.variantCombinations.forEach((v, i) => v.index = i + 1);

			this.product.variantCombinations = this.variantCombinations;

			(this.useGeneratedMatrix ? this.generatedMatrix : this.apiMatrix).forEach(variant => {
				if (Array.isArray(variant.productVariantIds)) {
					variant.productVariantIds = variant.productVariantIds.join(',');
				}
			});
			// Enrich variant data with inherited values and add productVariantIndexs
			this.product.variants = (this.useGeneratedMatrix ? this.generatedMatrix : this.apiMatrix)
				.map((variant, index) => ({
					...variant,
					description: variant.description || this.product.description,
					longDescription: variant.longDescription || this.product.longDescription,
					categoryId: variant.categoryId || this.product.categoryId,
					allowReturn: variant.allowReturn ?? this.product.allowReturn,
					returnPolicy: variant.returnPolicy || this.product.returnPolicy,
					isExpiry: variant.isExpiry ?? this.product.isExpiry,
					createdBy: variant.createdBy || this.product.createdBy,
					lastUpdatedBy: variant.lastUpdatedBy || this.product.lastUpdatedBy,
					lastUpdatedOn: this.product.lastUpdatedOn,
					productMedias: [...(this.product.productMedias || [])],
					productVariantIndexs: this.getProductVariantIndexes(variant),  // Use the function here
					productVariantIds: Array.isArray(variant.productVariantIds)
						? (variant.productVariantIds as number[]).join(',')
						: variant.productVariantIds || '',

				}));

			this.productService.save(this.product, this.mode).subscribe((data: any) => {
				if (data === 0) {
					this.toaster.warning('Record already exists.');
				} else if (data > 0) {
					this.toaster.success('Record submitted successfully.');
					this.router.navigate(['app/master/product/list']);
					this.cancel();
				}
			});
		} else {
			this.toaster.warning('Please provide valid input.');
		}
	}

	public cancel(): void {
		this.router.navigate(['app/master/product/list']);
	}


	fileUpload(event: any) {
		if (event) {
			const fileExtension = event.name.split('.').pop()?.toLowerCase();
			if (fileExtension === 'mp4') {
				this.productMediaModel.type = 4002; // Video 
			} else {
				this.productMediaModel.type = 4001; // Image 
			}
			this.productMediaModel.file = event;
		}
	}

	uploadMedia() {
		if (!this.productMediaModel.file || !this.productMediaModel.type) {
			this.toaster.error('Please select a valid file before uploading.', 'Upload Error');
			return;
		}
		this.product.productMedias.push(this.productMediaModel);
		this.productMediaModel = new ProductMediaModel();
	}

	deleteMedia(index: number) {
		this.product.productMedias.splice(index, 1);
	}

	// new code for variant combination
	public variantPropertyId: number = 0;
	public variantPropertyName: string = '';
	public variantValues: string[] = [];
	public editingIndex: number | null = null;
	public showVariantForm = false;
	public variantProperties: PropertyMainModel[] = [];

	add(): void {
		if (!this.access.canInsert) {
			this.toaster.warning('You do not have access to add.');
			return;
		}
		this.resetForm();
		this.showVariantForm = true;
	}
	trackByIndex(index: number, item: any): number {
		return index;
	}
	addValueField() {
		this.variantValues.push('');
	}
	removeValueField(index: number) {
		if (this.variantValues.length > 1) {
			this.variantValues.splice(index, 1);
		}
	}

	//  Client-side only save
	saveVariant() {
		if (!this.variantPropertyId || !this.variantValues || this.variantValues.length === 0) return;

		const selectedPropertyId = Number(this.variantPropertyId);
		const property = this.categoryPropertyModel.find(p => p.propertyId === selectedPropertyId);
		const propertyName = property?.propertyName || '';
		const trimmedNewValues = this.variantValues.map(v => v.trim());

		if (this.editingIndex !== null) {
			const currentIndex = this.editingIndex;

			// Get existing combinations for this index and property
			const existingCombinations = this.variantCombinations.filter(v =>
				v.variantPropertyId === selectedPropertyId && v.index === currentIndex
			);

			const existingValueMap = new Map(
				existingCombinations.map(v => [v.variantPropertyValue.toLowerCase(), v])
			);

			// Step 1: Keep only values that are still in the updated list
			this.variantCombinations = this.variantCombinations.filter(v =>
				v.variantPropertyId !== selectedPropertyId || v.index !== currentIndex ||
				trimmedNewValues.includes(v.variantPropertyValue)
			);

			// Step 2: Add new values that didn’t exist before (with new index and id: 0)
			let maxIndex = this.variantCombinations.length > 0
				? Math.max(...this.variantCombinations.map(v => v.index))
				: 0;

			for (const value of trimmedNewValues) {
				if (!existingValueMap.has(value.toLowerCase())) {
					this.variantCombinations.push({
						id: 0,
						index: ++maxIndex,
						productId: this.id,
						variantPropertyId: selectedPropertyId,
						variantPropertyName: propertyName,
						variantPropertyValue: value
					});
				}
			}

			this.toaster.success('Variant updated.');
			this.editingIndex = null;
		} else {
			// Add mode: add all values as new
			let maxIndex = this.variantCombinations.length > 0
				? Math.max(...this.variantCombinations.map(v => v.index))
				: 0;

			for (const value of trimmedNewValues) {
				this.variantCombinations.push({
					id: 0,
					index: ++maxIndex,
					productId: this.id,
					variantPropertyId: selectedPropertyId,
					variantPropertyName: propertyName,
					variantPropertyValue: value
				});
			}

			this.toaster.success('Variant added.');
		}

		this.resetForm();
	}

	editVariant(arrayIndex: number): void {
		const selectedVariant = this.variantCombinations[arrayIndex];
		if (!selectedVariant) {
			this.toaster.warning('Invalid variant selected for editing.');
			return;
		}

		this.variantPropertyId = selectedVariant.variantPropertyId;
		this.variantPropertyName = selectedVariant.variantPropertyName;

		// Get all values for the same propertyId and index
		this.variantValues = this.variantCombinations
			.filter(v => v.variantPropertyId === selectedVariant.variantPropertyId && v.index === selectedVariant.index)
			.map(v => v.variantPropertyValue);

		this.editingIndex = selectedVariant.index;
		this.showVariantForm = true;
	}
	removeVariant(arrayIndex: number): void {
		const selectedVariant = this.variantCombinations[arrayIndex];
		if (!selectedVariant) return;

		const confirmDelete = confirm('Are you sure you want to delete this variant?');
		if (!confirmDelete) return;

		this.variantCombinations = this.variantCombinations.filter(
			v => !(v.variantPropertyId === selectedVariant.variantPropertyId && v.index === selectedVariant.index)
		);

		this.toaster.success('Variant removed.');
	}
	onPropertyChange(propertyId: number) {
		const selectedProperty = this.categoryPropertyModel.find(p => p.propertyId === +propertyId);
		this.variantPropertyName = selectedProperty ? selectedProperty.propertyName : '';
	}

	resetForm(): void {
		this.variantPropertyId = 0;
		this.variantPropertyName = '';
		this.variantValues = [];
		this.editingIndex = null;
		this.showVariantForm = false;
	}
	onCategoryChanged(categoryId: number) {
		this.loadPropertiesByCategory(categoryId);
	}
	loadPropertiesByCategory(categoryId: number) {
		if (!categoryId) {
			this.categoryPropertyModel = [];
			return;
		}

		this.categoryPropertyParameterModel.categoryId = categoryId;
		this.categoryPropertyService.getForCategoryProperty(this.categoryPropertyParameterModel).subscribe(data => {
			this.categoryPropertyModel = data;
		});
	}

	toggleVariantForm(): void {
		this.resetForm();
		this.showVariantForm = !this.showVariantForm;
	}

	//CODE FOR THE MATRIX ITEM
	public useGeneratedMatrix: boolean = false;
	public apiMatrix: ProductModel[] = []; // Original from API
	public generatedMatrix: ProductModel[] = []; // After clicking refresh button

	generateVariantMatrix(): any[] {
		const grouped: { [key: string]: string[] } = {};

		this.variantCombinations.forEach((variant) => {
			const key = variant.variantPropertyName;
			const values = variant.variantPropertyValue
				.split(',')
				.map((v) => v.trim())
				.filter((v) => v);

			if (!grouped[key]) {
				grouped[key] = values;
			} else {
				grouped[key] = Array.from(new Set([...grouped[key], ...values]));
			}
		});

		const keys = Object.keys(grouped);

		function cartesian(arr: string[][]): string[][] {
			return arr.reduce<string[][]>(
				(a, b) => a.flatMap((d) => b.map((e) => [...d, e])),
				[[]] as string[][],
			);
		}

		const combinations = cartesian(keys.map(k => grouped[k]));

		return combinations.map((combo, index) => {
			const combinationIndexes = combo.map((value) => {
				const match = this.variantCombinations.find(
					(vc) => vc.variantPropertyValue.trim() === value.trim()
				);
				return match ? match.index : 0;
			});

			const combinationVariantIds = combo.map((value, i) => {
				const variant = this.variantCombinations.find(
					(v) => v.variantPropertyName === keys[i] && v.variantPropertyValue.trim() === value.trim()
				);
				return variant?.id || 0;
			}).filter(id => id !== 0);

			return {
				name: `${this.product.name} ${combo.join(' ')}`,
				sku: `${this.product.sku}-${combo.join('-')}`,
				upc: Number(`${this.product.id}${1000 + index}`),
				finalSellPrice: this.product.finalSellPrice,
				description: this.product.description,
				longDescription: this.product.longDescription,
				categoryId: this.product.categoryId,
				allowReturn: this.product.allowReturn,
				returnPolicy: this.product.returnPolicy,
				isExpiry: this.product.isExpiry,
				createdBy: this.product.createdBy,
				lastUpdatedBy: this.product.lastUpdatedBy,
				lastUpdatedOn: this.product.lastUpdatedOn,
				productMedias: this.product.productMedias,
				productVariantIndexs: combinationIndexes,
				productApiVariantIds: combinationVariantIds,
				productVariantIds: combinationVariantIds,
			};
		});

	}

	public refreshMatrix(): void {
		const generated = this.generateVariantMatrix();
		this.generatedMatrix = generated.map((generatedItem, index) => {
			const existing = this.apiMatrix.find(apiItem => apiItem.name === generatedItem.name);
			const enrichedItem = {
				...generatedItem,
				productVariantIndexs: generatedItem.productVariantIndexs,
				productApiVariantIds: generatedItem.productApiVariantIds,
			};
			return existing ? { ...existing, ...enrichedItem } : enrichedItem;
		});
		this.useGeneratedMatrix = true;
	}

	// Helper method for joining productCombinationIds
	getProductVariantIndexes(item: any): number[] {
		if (item && Array.isArray(item.productVariantIndexs) && item.productVariantIndexs.length > 0) {
			return item.productVariantIndexs;
		}
		return [];
	}


	getProductCombinationIds(item: any): string {
		if (item && Array.isArray(item.productVariantIndexs) && item.productVariantIndexs.length > 0) {
			return item.productVariantIndexs.join(', ');
		}
		return '';
	}
	getProductApiVariantIds(item: any): string {
		if (item && Array.isArray(item.productApiVariantIds) && item.productApiVariantIds.length > 0) {
			return item.productApiVariantIds.join(', ');
		}
		return '';
	}
	resetMatrix(): void {
		this.useGeneratedMatrix = false;
	}

	public deleteMatrix(id: number): void {
		if (!this.access.canDelete) {
			this.toaster.warning('You do not have delete access for this page.');
			return;
		}

		if (window.confirm('Are you sure you want to delete this product?')) {
			this.productService.delete(id).subscribe(data => {
				this.toaster.success('Product Variant deleted successfully.');
				this.setPageEditMode();
			});
		}
	}
	public editMatrix(id: number): void {
		if (!this.access.canUpdate) {
			this.toaster.warning('You do not have edit access for this page.');
			return;
		}
		this.router.navigate(['app/master/product/edit', id]);
	}
	//sync button in edit mode
	public sync(id: number): void {
		this.productParameterModel.id = id;
		this.productService.sync(this.productParameterModel).subscribe(data => {
			this.toaster.success('Product Sync successfully.');
		});
	}

	public syncAll(): void {
		const matrix = this.apiMatrix;
		matrix.forEach(item => {
			if (item.id) {
				this.sync(item.id);
			}
		});
	}

}
