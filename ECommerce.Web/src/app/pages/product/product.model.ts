import { FileModel } from "../../models/file.model";
import { PagingSortingModel } from "../../models/pagingsorting.model";


export class ProductInventoryModel {
    //public id: number = 0;
    public productId: number = 0;
    public openingQty: number = 0;
    public buyQty: number = 0;
    public lockQty: number = 0;
    public orderQty: number = 0;
    public sellQty: number = 0;
    public closingQty: number = 0;
    public costPrice: number = 0.0;
    public sellPrice: number = 0.0;
    public discountPercentage: number = 0.0;
    public discountAmount: number = 0.0;
    public finalSellPrice: number = 0.0;
}
export class ProductModel extends ProductInventoryModel {
    public id: number = 0;
    public name: string = '';
    public description: string = '';
    public longDescription: string = '';
    public categoryId: number = 0;
    public categoryName: string = '';
    public groupProductId: number = 0;
    public allowReturn: boolean = false;
    public returnPolicy: string = '';
    public isExpiry: boolean = false;
    public mediaThumbUrl: string = '';
    public createdBy: number = 0;
    public createdOn: Date = new Date();
    public lastUpdatedBy: number = 0;
    public lastUpdatedOn: Date = new Date();
    public sku: string = '';
    public upc: number = 0;

    //extra 
    public rating: number = 0;
    public reviews: number = 0;
    public originalPrice: number = 0;
    public category: number = 0;
    public reviewsCount: number = 0;
    public averageRating: number = 0;
    public colors: any = [];
    public sizes: any = [];
    public availableSizes: any = [];
    public features: any = [];
    public isWishlisted: boolean = false;
    public thumbUrl: string = '';


    public productMedias: ProductMediaModel[] = [];
    public properties: any[] = [];
}
export class ProductMediaModel {
    public id: number = 0;
    public productId: number = 0;
    public type: number = 0;
    public url: string = '';
    public thumbUrl: string = '';
    public description: string = '';
    public file: FileModel = new FileModel();
}

export class ProductMainModel {
    public id: number = 0;
    public name: string = '';
}
export class CategoryMainModel {
    public id: number = 0;
    public name: string = '';
}
export class ProductAddModel {
    public categories: CategoryMainModel[] = [];
    public groupProducts: GroupProductMainModel[] = [];
}
export class ProductEditModel extends ProductAddModel {
    public product: ProductModel = new ProductModel();
}
export class ProductGridModel {
    public products: ProductModel[] = [];
    public totalRecords: number = 0;
}
export class ProductListModel extends ProductGridModel {
    public categories: CategoryMainModel[] = [];
    public groupProducts: GroupProductMainModel[] = [];
}
export class GroupProductMainModel {
    public id: number = 0;
    public name: string = '';
}
export class ProductDetailsModel {
    public id: number = 0;
    public name: string = '';
    public description: string = '';
    public sku: string = '';
    public sellPrice: number = 0;
    public discountPercentage: number = 0;
    public finalSellPrice: number = 0;
    public categoryName: string = '';
    public url: string = '';
    public thumbUrl: string = '';
    public productMedias: [] = [];
    //extra
    public isExpiry: boolean = false;

}

export class ProductParameterModel extends PagingSortingModel {
    public id: number = 0;
    public name: string = '';
    public description: string = '';
    public categoryId: number = 0;
    public groupProductId: number = 0;
}

export class ProductDetailsSpecificationModel {
    public propertyName: string = '';
    public unit: string = '';
    public value: string = '';
}
export class ProductVarientModel {
    public id: number = 0;
    public variantPropertyId: number = 0;
    public variantPropertyValue: string = '';
    public variantPropertyName: String = '';
    public productId: number = 0;
}

export class ProductAllVarientModel {
    public id: number = 0;
    public name: String = '';
    public sku: string = '';
    public productVariantIds: String  = '';
    public finalSellPrice: number = 0;
    public imageUrl: string = '';
}

export class ProductDetailsGridModel {
    public productDetails: ProductDetailsModel[] = [];
    public productDetailsSpecification: ProductDetailsSpecificationModel[] = [];
    public productVarient: ProductVarientModel[] = [];
    public productAllVarient: ProductAllVarientModel[] = [];
}

export class ProductDetailsParameterModel {
    public id: number = 0;
}

