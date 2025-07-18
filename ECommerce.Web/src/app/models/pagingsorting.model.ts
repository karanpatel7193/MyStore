import { enumExport } from "../enum";

export class PagingSortingModel {
    public sortExpression: string = 'id';
    public sortDirection: string = 'asc';
    public pageIndex: number = 1;
    public pageSize: number = 10;
    public exportType: enumExport = enumExport.Excel;
    public totalRecords: number = 0;
}
