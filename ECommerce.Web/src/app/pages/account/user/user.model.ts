import { PagingSortingModel } from '../../../models/pagingsorting.model';
import { MasterValuesModel } from '../../../models/mastervalue.model';
export class RoleMainModel {
    public id: number = 0;
    public name: string = '';
}
export class UserMainModel {
    public id: number = 0;
    public firstName: string = '';
    public lastName: string = '';
    public email: string = '';
}
export class RoleMenuAccessModel {
    constructor(data: any = null) {
        if (data != null) {
            this.id = data.id;
            this.roleId = data.roleId;
            this.menuId = data.menuId;
            this.canInsert = data.canInsert;
            this.canUpdate = data.canUpdate;
            this.canDelete = data.canDelete;
            this.canView = data.canView;
            this.parentIdName = data.parentIdName;
            this.menuIdName = data.menuIdName;
        }
    }

    public id: number = 0;
    public roleId: number = 0;
    public menuId: number = 0;
    public canInsert: boolean = false;
    public canUpdate: boolean = false;
    public canDelete: boolean = false;
    public canView: boolean = false;
    public parentIdName: string = '';
    public menuIdName:string = '';
}
export class UserModel extends UserMainModel {
    public middleName: string = '';
    public roleId: number = 0;
    public roleName: string = '';
    public username: string = '';
    public password: string = '';
    public OldPassword: string = '';
    public passwordSalt: string = '';
    public phoneNumber: string = '';
    public birthDate: Date = new  Date(0);
    public gender: number = 0;
    public imageSrc: string = '';
    public lastUpdateDateTime: Date = new Date();
    public isActive: boolean = true;
    public confirmPassword: string = '';
    public mode: string = 'Admin';
    public type: string = 'Individual';
    public pmsName: string = '';
}

export class UserAddModel {
    public roles: RoleMainModel[] = [];
}

export class UserEditModel extends UserAddModel {
    public user: UserModel = new UserModel();
}

export class UserGridModel {
    public users: UserModel[] = [];
    public totalRecords: number = 0;
}

export class UserListModel extends UserGridModel {
    public roles: RoleMainModel[] = [];
}

export class UserParameterModel extends PagingSortingModel {
    public id: number = 0;
    public firstName: string = '';
    public lastName: string = '';
    public email: string = '';
    public roleId: number = 0;;
    public username: string = '';
    public phoneNumber: string = '';
    public parentUserId: number = 0;

}

export class UserLoginModel extends UserMainModel {
    public username: string = '';
    public token: string = '';
    public gender: number = 0;
    public roleId: number = 0;
    public roleName: string = '';
    public imageSrc: string = '';
    public roleMenuAccesss: RoleMenuAccessModel[] = [];
    public masterValues: MasterValuesModel[] = [];
    public pmsName: string = '';

}
export class UserUpdateModel {
    id: number = 0
    firstName: string = ''
    lastName: string = ''
    username: string = ''
    email: string = ''
}


