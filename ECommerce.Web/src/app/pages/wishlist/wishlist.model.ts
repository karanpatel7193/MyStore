import { PagingSortingModel } from "../../models/pagingsorting.model";
import { ProductModel } from "../product/product.model";

export class WishlistModel {
    public id: number = 0;
    public userId: number = 0;
    public productId: number = 0;
    public createdTime: Date = new Date();
}

export class WishlistProductModel {
    public id: number = 0;
    public userId: number = 0;
    public productId: number = 0;
    public mediaThumbUrl: string = '';
    public name: string = '';
    public finalSellPrice: number = 0;
    public isExpiry: boolean = false;

}

export class WishlistGridModel {
    public wishlists: WishlistProductModel[] = [];
    public totalRecords: number = 0;
}

export class WishlistParameterModel extends PagingSortingModel {
    public productId: number = 0;
    public userId: number = 0;
}
