import { PagingSortingModel } from "src/app/models/pagingsorting.model";
import { ProductMainModel } from "../../master/product/product.model";
import { VendorMainModel } from "../../vendor/vendor/vendor.model";

export class PurchaseInvoiceMainModel {
    public id: number = 0;
    public name: string = '';
}

export class PurchaseInvoiceItemModel {
    public id: number = 0;
    public purchaseInvoiceId: number = 0;
    public productId: number = 0;
    public productName: string = '';
    public quantity: number = 0;
    public price: number = 0;
    public amount: number = 0;
    public discountPercentage: number = 0;
    public discountedAmount: number = 0;
    public tax: number = 0;
    public finalAmount: number = 0;
    public expiryDate: Date | null = null;
    public isExpiry: boolean = false ;
    taxDiscountType: 'amount' | 'percentage' = 'amount'; 
    discountType: 'amount' | 'percentage' = 'amount';
}

export class PurchaseInvoiceModel extends PurchaseInvoiceMainModel {
    public invoiceNumber: number = 0;
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

    public purchaseInvoiceItems: PurchaseInvoiceItemModel[] = [];
}

export class PurchaseInvoiceGridModel {
    public invoices: PurchaseInvoiceModel[] = [];
    public totalRecords: number = 0;
}

export class PurchaseInvoiceAddModel {
    public vendors: VendorMainModel[] = [];
    public products: ProductMainModel[] = [];
}

export class PurchaseInvoiceEditModel extends PurchaseInvoiceAddModel {
    public purchaseInvoice: PurchaseInvoiceModel = new PurchaseInvoiceModel();
}

export class PurchaseInvoiceListModel extends PurchaseInvoiceGridModel {
    public vendors: VendorMainModel[] = [];
    public products: ProductMainModel[] = [];
}

export class PurchaseInvoiceParameterModel extends PagingSortingModel {
    public id: number = 0;
    public description: string = '';
    public vendorId: number = 0;
    public totalQuantity: number = 0;
    public totalAmount: number = 0;
    public totalDiscount: number = 0;
    public totalTax: number = 0;
    public totalFinalAmount: number = 0;
    public productId: number = 0;
    public vendorName: string = '';
    public productName: string = '';
}
