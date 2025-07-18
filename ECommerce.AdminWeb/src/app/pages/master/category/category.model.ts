import { FileModel } from "src/app/models/file.model";
import { PagingSortingModel } from "src/app/models/pagingsorting.model";

export class CategoryMainModel {
    public id: number = 0;
    public name: string = '';
}

export class CategoryModel {
    public id: number = 0;
    public name: string = '';
    public parentId: number = 0;
    public parentName: string = '';
    public imageUrl: string = '';
    public description: string = '';
    public isVisible: boolean = false;
    public file: FileModel = new FileModel();
}

// export class CategoryImageModel extends FileModel{
//     public categoryId: number = 0;
// }

export class CategoryAddModel {
    public parentCategories: CategoryMainModel[] = [];
}

export class CategoryEditModel extends CategoryAddModel{
    public category: CategoryModel = new CategoryModel;
}

export class CategoryGridModel {
    public categories: CategoryModel[] = [];
    public totalRecords: number = 0;
}

export class CategoryListModel extends CategoryGridModel{
}

export class CategoryParameterModel extends PagingSortingModel {
    public name: string = '';
    public id: number = 0;
    public parentId: number = 0;
}


