export class PropertyMainModel {
    public propertyId: number = 0;
    public propertyName: string = '';
}

export class PropertyModel {
    public propertyId: number = 0;
    public value: string = '';
    public unit: string = '';
}

export class SearchGridModel {
    public properties: PropertyMainModel[] = [];
    public values: PropertyModel[] = [];
}
export class SearchPropertyParameterModel {
    public propertyId: number = 0;
    public propertyName: string = '';
    public values: string[] = [];
}

export class SearchProductParameterModel {
    public categoryId: number = 0;
    public searchProperties: SearchPropertyParameterModel[] = [];
}

export class SearchPropertyMongoModel {
    public id: string = '';
    public mediaThumbUrl: string = '';
    public name: string = '';
    public description: string = '';
    public price: number = 0;
}

export class FilterModel {
    public categoryId: number = 0;
    public propertyId: number = 0;
    public propertyName: string = '';
    public values: string[] = [];

}