import { PagingSortingModel } from "src/app/models/pagingsorting.model";
import { PropertyMainModel } from "../property/property.model";

export class categoryPropertyMainModel{
    id: number = 0;
    name: string = ''
}
export class CategoryPropertyModel extends categoryPropertyMainModel {
    categoryId: number = 0
    propertyId: number = 0
    propertyName: string = ''
    unit: string =''
    categoryName: string =''
}

export class CategoryPropertyGridModel {
    categoryPropertys: CategoryPropertyModel[] = []
    totalRecords: number = 0
}

export class CategoryPropertyParameterModel extends PagingSortingModel {
    categoryId: number =0
    propertyId: number =0
    unit: string =''
}

export class CategoryPropertyAddModel {
    properties: PropertyMainModel[] = [];
}

export class CategoryPropertyEditModel extends CategoryPropertyAddModel {
    categoryProperty: CategoryPropertyModel = new CategoryPropertyModel();
}

export class CategoryPropertyListModel extends CategoryPropertyGridModel {
    categoryProperties: CategoryPropertyModel[] = [];
}
