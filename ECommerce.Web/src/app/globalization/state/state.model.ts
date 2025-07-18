import { CountryMainModel } from "../../models/country.model";

export class StateEntity {
    // Define properties for StateEntity if needed
    public countries: CountryMainModel[] = [];

  }
  
  export class StateMainModel {
    public id: number = 0;
    public name: string = '';
  }
  
  export class StateParameterModel {
    public countryId: number = 1;
  }
  
  