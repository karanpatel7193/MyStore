import { CountryMainModel } from "src/app/models/country.model";
import { PagingSortingModel } from "src/app/models/pagingsorting.model";
import { StateMainModel } from "src/app/pages/globalization/state/state.model";

export class VendorMainModel {
    public id: number = 0;
    public name: string = '';
}
export class VendorModel extends VendorMainModel {
    public email: string = '';
    public phone: number = 0;
    public address: string = '';
    public stateId: number = 0;
    public countryId: number = 0;
    public postalCode: string = '';
    public status: boolean = false;
    public taxNumber: string = '';
    public bankAccountNumber: string = '';
    public bankName: string = '';
    public ifscCode: string = '';
    public contactPersonName: string = '';
    public contactPersonPhone: string = '';
    public createdOn: Date = new Date();
    public totalOutstanding: number = 0;
    public totalPaid: number = 0;
    public totalInvoices: number = 0;
}
export class VendorAddModel {
    public countries: CountryMainModel[] = [];
}

export class VendorEditModel extends VendorAddModel {
    public vendor: VendorModel = new VendorModel();
    public states: StateMainModel[] = [];
}

export class VendorGridModel {
    public vendors: VendorModel[] = [];
    public totalRecords: number = 0;
}


export class VendorListModel extends VendorGridModel {
    public countries: CountryMainModel[] = [];
    public states: StateMainModel[] = [];
}

export class VendorParameterModel extends PagingSortingModel {
    public id: number = 0;
    public name: string = '';
    public email: string = '';
    public phone: number = 0;
    public postalCode: string = '';
    public countryId: number = 0;
    public stateId: number = 0;
}
