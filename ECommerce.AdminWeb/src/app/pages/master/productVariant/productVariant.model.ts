export class ProductVariantModel {
    public index: number = 0;
    public id: number = 0;
    public productId: number = 0;
    public variantPropertyId: number = 0;
    public variantPropertyValue: string = '';
    public variantPropertyName: string = '';
}

export class ProductVariantGridModel {
    public variantCombinations: ProductVariantModel[] = [];
}

export class ProductVariantParameterModel {
    public productId: number = 0;
}
