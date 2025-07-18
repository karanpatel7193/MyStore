import { PagingSortingModel } from "../../../models/pagingsorting.model";

export class ReviewModel{
    id: number = 0;
    userId: number = 0;
    productId: number = 0;
    rating: number = 0;
    comments: string = '';
    mediaList: ReviewMediaModel[] = [];
    //extra data 
    userName: string = '';
    date: Date = new Date();
    

}

export class ReviewMediaModel{
    id: number = 0;
    reviewId: number = 0;
    mediaType: string = '';
    mediaURL: string = '';
}

export class ReviewGridModel{
    reviews: ReviewModel[] = [];
    mediaList: ReviewMediaModel[] = [];
    totalRecords: number = 0;
}

export class ReviewParameterModel extends PagingSortingModel {
    productId: number = 0;
    userId: number = 0;
}

