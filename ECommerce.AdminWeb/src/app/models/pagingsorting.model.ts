import { enumExport } from "../enum";

export class PagingSortingModel {
    public sortExpression: string = '';
    public sortDirection: string = 'desc';
    public pageIndex: number = 1;
    public pageSize: number = 10;
    public exportType: enumExport = enumExport.Excel;
    public totalRecords: number = 0;
}
