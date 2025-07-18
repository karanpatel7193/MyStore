export class AddressModel {
    public id: number = 0;
    public userId: number = 0;
    public fullName: string = '';
    public mobileNumber: string = '';
    public alternateNumber: string = '';
    public addressLine: string = '';
    public landmark: string = '';
    public cityId: number = 0;
    public stateId: number = 0;
    public countryId: number = 0;
    public pinCode: string = '';
    public addressType: string = '';
    public isDefault: boolean = false;
    public cityName: string = '';
    public stateName: string = '';

  }
  
  export class AddressGridModel {
    public addresses: AddressModel[] = [];
    public totalRecords: number = 0;
  }
  
  export class AddressParameterModel {
    public id: number = 0;
    public userId: number = 0;
  }
  