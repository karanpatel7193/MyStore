import { PagingSortingModel } from "src/app/models/pagingsorting.model";
import { CategoryMainModel } from "../category/category.model";
import { FileModel } from "src/app/models/file.model";
import { ProductVariantModel } from "../productVariant/productVariant.model";



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
    public price: number = 0;
    public longDescription: string = '';
    public categoryId: number = 0;
    public categoryName: string = '';
    public parentProductId: number = 0;
    public allowReturn: boolean = false;
    public returnPolicy: string = '';
    public isExpiry: boolean = false ;
    public thumbUrl: string = '';


    public productVariantIds: string = '';
    // public productVariantIds: number[] = [];
    public productCombinationIds: number[] = [];
    public productVariantIndexs: number[] = [];     //combination is like [1,5]
    
    public createdBy: number = 0;
    public createdOn: Date  = new Date(); 
    public lastUpdatedBy: number = 0;
    public lastUpdatedOn: Date = new Date();
    public sku: string = '';
    public upc: number = 0;

    public productMedias: ProductMediaModel[] = [];
    public variantCombinations: ProductVariantModel[] = [];
    public variants: ProductModel[] = [];
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

export class ProductAddModel {
    public categories: CategoryMainModel[] = [];
    public groupProducts: GroupProductMainModel[] = [];
}
export class ProductEditModel extends ProductAddModel {
    public product: ProductModel = new ProductModel();
    public variantCombinations: ProductVariantModel[] = [];
    public variants: ProductModel[] = [];
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


export class ProductParameterModel extends PagingSortingModel {
    public id: number = 0;
    public name: string = '';
    public description: string = '';
    public categoryId: number = 0;
    public groupProductId: number = 0;
}
