
import { CountryMainModel } from 'src/app/models/country.model';
import { PagingSortingModel } from '../../../models/pagingsorting.model';
import { CityMainModel } from '../../globalization/city/city.model';
import { StateMainModel } from '../../globalization/state/state.model';

export class EmployeeMainModel {
    public id: number = 0;

}

export class EmployeeModel extends EmployeeMainModel {
    public firstName: string = '';
    public middleName: string = '';
    public lastName: string = '';
    public gender: string = '';
    public email: string = '';
    public phoneNumber: string = '';
    public dob: Date = new Date(0);
    public dateOfJoin: Date = new Date(0);
    public education: string = '';
    public cityId: number = 0;
    public stateId: number = 0;
    public countryId: number = 0;

}

export class EmployeeAddModel {
    public states: StateMainModel[] = [];
    public citys: CityMainModel[] = [];
    public countrys: CountryMainModel[] = [];

}

export class EmployeeEditModel extends EmployeeAddModel {
    public employee: EmployeeModel = new EmployeeModel();
}

export class EmployeeGridModel {
    public employees: EmployeeModel[] = [];
    public totalRecords: number = 0;
}

export class EmployeeListModel extends EmployeeGridModel {
    public states: StateMainModel[] = [];
    public citys: CityMainModel[] = [];

}

export class EmployeeParameterModel extends PagingSortingModel {
    public id?: number = 0;
    public dob?: Date ;
    public cityId?: number = 0;
    public stateId?: number = 0;
    public countryId: number = 0;

}
