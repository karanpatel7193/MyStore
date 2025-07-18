using ECommerce.Entity.Master;
using System.Data;

namespace ECommerce.Repository.Account
{
    public  interface IUserReppository: IBusiness<UserEntity, UserMainEntity, UserAddEntity, UserEditEntity, UserGridEntity, UserListEntity, UserParameterEntity, long>
    {
        public  Task<long> Registration(UserEntity userEntity);
        public  Task<List<UsersEntity>> SelectForUsersTest(); //TODO: Check and delete if not require.
        public  Task<int> UserUpdate(UserUpdateEntity userUpdateEntity);
        public  Task<UserLoginEntity> ValidateLogin(UserEntity userEntity);
        public  Task<bool> UpdateActive(UserEntity userEntity);
        public  Task<bool> UpdatePassword(UserEntity userEntity);
        public  Task<bool> ChangePassword(UserEntity userEntity);
        public  Task<bool> UpdatePasswordDirect(UserEntity userEntity);
        public Task<UserEntity> ResetPassword(string UsernameEmail);

    }
}
