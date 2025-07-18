import { PagingSortingModel } from "src/app/models/pagingsorting.model";
import { PropertyMainModel } from "../property/property.model";

export class GroupProductPropertyMainModel {
    id: number = 0;
    name: string = ''
}
export class GroupProductPropertyModel  extends GroupProductPropertyMainModel  {
    groupProductId: number = 0
    propertyId: number = 0
    groupProductName: string = ''
}

export class GroupProductPropertyGridModel  {
    groupProductProperty: GroupProductPropertyModel [] = []
    totalRecords: number = 0
}

export class GroupProductPropertyParameterModel  extends PagingSortingModel {
    groupProductId: number =0
    propertyId: number =0
}

export class GroupProductPropertyAddModel {
    properties: PropertyMainModel[] = [];
}

export class GroupProductPropertyEditModel extends GroupProductPropertyAddModel {
    groupProductProperty: GroupProductPropertyModel  = new GroupProductPropertyModel ();
}

export class GroupProductPropertyListModel extends GroupProductPropertyGridModel {
    groupProductProperties: GroupProductPropertyModel[] = [];
}
