import { CountryMainModel } from "src/app/models/country.model";

export class CityEntity {
    // Define properties for CityEntity if needed
    public countries: CountryMainModel[] = [];

  }
  
  export class CityMainModel {
    public id: number = 0;
    public name: string = '';
  }
  
  export class CityParameterModel {
    public countryId: number = 0;
  }
  
  