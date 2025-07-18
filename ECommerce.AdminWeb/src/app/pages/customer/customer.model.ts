import { PagingSortingModel } from "src/app/models/pagingsorting.model";

export class CustomerMainModel {
    public id: number = 0;
    public name: string = '';
}

export class CustomerModel extends CustomerMainModel {
    public totalBuy: number = 0;
    public totalInvoices: string = '';
    public status: number = 0;
    public userId: number = 0;
    public userName: string = '';

}

export class CustomerGridModel {
    public customers: CustomerModel[] = [];
    public totalRecords: number = 0;
}

export class CustomerParameterModel extends PagingSortingModel {
    public name: string = '';
    public status: number = 0;
    public userId: number = 0;
}
