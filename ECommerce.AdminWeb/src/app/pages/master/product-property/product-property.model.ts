import { PagingSortingModel } from "src/app/models/pagingsorting.model";

export class ProductPropertyMainModel{
    public id: number = 0;
    public propertyId: number = 0;
}
export class ProductPropertyModel extends ProductPropertyMainModel {
    public productId: number = 0
    public propertyName: string =''
    public unit: string =''
    public value: string =''
}
export class VariantProductPropertyModel
{
    public variantPropertyId: number = 0
}

export class ProductPropertyGridModel {
    public productProperties: ProductPropertyModel[] = []
    public variantPropertyIds: VariantProductPropertyModel[] = []
}

export class ProductPropertyParameterModel extends PagingSortingModel {
    public id: number = 0
    public productId: number = 0
    public categoryId: number = 0
    public productProperties: ProductPropertyModel[] = []
}
