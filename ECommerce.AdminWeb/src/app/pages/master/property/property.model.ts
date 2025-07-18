import { PagingSortingModel } from "src/app/models/pagingsorting.model";

export class PropertyMainModel {
    public id: number = 0;
    public name: string = '';
}

export class PropertyModel extends PropertyMainModel{
    public description: string = '';
}

export class PropertyGridModel {
    public properties: PropertyModel[] = [];
    public totalRecords: number = 0;
}

export class PropertyParameterModel extends PagingSortingModel {
    public id: number = 0;
    public name: string = '';
}
