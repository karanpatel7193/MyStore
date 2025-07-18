export class BlockModel {
    public id: number = 0;
    public name: string = '';
    public content: string = '';
    public description: string = '';
}

export class BlockGridModel {
    public blocks: BlockModel[] = [];
    public products: BlockProductModel[] = [];
}

export class BlockProductModel {
    public blockId: number = 0;
    public productId: number = 0;
    public productName: string = '';
    public thumbUrl: string = '';
    public finalSellPrice: number = 0;
    public discountPercentage: number = 0;
    public discountAmount: number = 0;
}
