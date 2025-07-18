export class CategoryModel {
    public id: number = 0;
    public name: string = '';
    public parentId: number = 0;
    public imageUrl: string = '';
    //extra
    public expanded:boolean = false;
    subCategories?: CategoryModel[]; 

}

export class CategoryGridModel {
    public categories: CategoryModel[] = [];
}

export class CategoryParameterModel {
    public id: number = 0;
    public name: string = '';
    public parentId: number = 0;
}
