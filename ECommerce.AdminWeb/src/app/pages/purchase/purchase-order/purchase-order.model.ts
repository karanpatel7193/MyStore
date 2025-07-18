import { PagingSortingModel } from "src/app/models/pagingsorting.model";
import { VendorMainModel } from "../../vendor/vendor/vendor.model";
import { ProductMainModel } from "../../master/product/product.model";

export class PurchaseOrderMainModel {
    public id: number = 0;
    public name: string = '';

}

export class PurchaseOrderItemModel extends PurchaseOrderMainModel {
    public purchaseOrderId: number = 0;
    public productId: number = 0;
    public productName: string = '';
    public quantity: number = 0;
    public price: number = 0;
    public amount: number = 0;
    public discountPercentage: number = 0;
    public discountedAmount: number = 0;
    public tax: number = 0;
    public finalAmount: number = 0;
    public expiryDate: Date = new Date();
    taxDiscountType: 'amount' | 'percentage' = 'amount'; 
    discountType: 'amount' | 'percentage' = 'amount';
}

export class PurchaseOrderModel extends PurchaseOrderMainModel  {
    public orderNumber: number = 0;
    public createdBy: number = 0;
    public createdOn: Date = new Date();
    public lastUpdatedBy: number = 0;
    public lastUpdatedOn: Date = new Date();
    public vendorId: number = 0;
    public vendorName: string = '';
    public description: string = '';
    public totalQuantity: number = 0;
    public totalAmount: number = 0;
    public totalDiscount: number = 0;
    public totalTax: number = 0;
    public totalFinalAmount: number = 0;
    public purchaseOrderItem: PurchaseOrderItemModel[] = [];
}

export class PurchaseOrderGridModel {
    public orderInvoices: PurchaseOrderModel[] = [];
    public totalRecords: number = 0;
}

export class PurchaseOrderAddModel {
    public vendors: VendorMainModel[] = [];
    public products: ProductMainModel[] = [];
}

export class PurchaseOrderEditModel extends PurchaseOrderAddModel {
    public purchaseOrder: PurchaseOrderModel = new PurchaseOrderModel();
}

export class PurchaseOrderListModel extends PurchaseOrderGridModel {
    public vendors: VendorMainModel[] = [];
    public products: ProductMainModel[] = [];
}

export class PurchaseOrderParameterModel extends PagingSortingModel {
    public id: number = 0;
    public description: string = '';
    public vendorName: string = '';
    public vendorId: number = 0;
    public totalQuantity: number = 0;
    public totalAmount: number = 0;
    public totalDiscount: number = 0;
    public totalTax: number = 0;
    public totalFinalAmount: number = 0;
    public productId: number = 0;
}

