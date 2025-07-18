import { FileModel } from "src/app/models/file.model";
import { PagingSortingModel } from "src/app/models/pagingsorting.model";
import { ProductMainModel } from "../../master/product/product.model";

export class BlockMainModel {
    public id: number = 0;
    public name: string = '';
}

export class BlockModel extends BlockMainModel {
    public description: string = '';
    public isActive: boolean = false;
    public content: string = '';
    public blockProducts: BlockProductModel[] = []; 
}

export class BlockGridModel {
    public blocks: BlockModel[] = [];
    public totalRecords: number = 0;
}

export class BlockAddModel {
    public products: ProductMainModel[] = []; 
}

export class BlockEditModel extends BlockAddModel {
    public block: BlockModel = new BlockModel();
}

export class BlockParameterModel extends PagingSortingModel {
    public id: number = 0;
    public name: string = '';
    public description: string = '';
    public isActive: boolean = false;
}
export class BlockListModel extends BlockGridModel {
}

export class BlockProductModel{
    public id: number = 0;
    public blockId: number = 0;
    public productId: number = 0; 
    public productName: string = ''; 
}
