import { PagingSortingModel } from "../../models/pagingsorting.model";

export class CartMainModel {
  public userId: number = 0;
  public cartItems: CartModel[] = [];
}
export class CartModel {
  public id: number = 0;
  public userId: number = 0;
  public productId: number = 0;
  public quantity: number = 0;
  public price: number = 0;
  public offerPrice: number = 0;
  public discountAmount: number = 0;
  public isActive: boolean = true;
  public addedDate: Date = new Date();
  public productName: string = '';
  public sku: string = '';
  public mediaThumbUrl: string = '';
  public totalPrice: number = 0; // New property for computed price

}

export class CartGridModel {
  public products: CartModel[] = [];
  public totalRecords: number = 0;

  //for the ui side logic 
  public subTotal: number = 0; 
  public total: number = 0; 
  public discount: number = 0; 
}

export class CartParameterModel extends PagingSortingModel {
  public userId: number = 0;
  public productId: number = 0;
  public productName: string = '';
}
