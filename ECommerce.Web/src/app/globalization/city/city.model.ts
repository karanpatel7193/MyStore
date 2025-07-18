export class CityModel {
  // Define properties for CityEntity if needed
  public cities: CityMainModel[] = [];
}

export class CityMainModel {
  public id: number = 0;
  public name: string = '';
}

export class CityParameterModel {
  public stateId: number = 0;
}
