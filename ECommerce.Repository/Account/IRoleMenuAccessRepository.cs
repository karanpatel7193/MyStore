using ECommerce.Entity.Account;

namespace ECommerce.Repository.Account
{
    public interface IRoleMenuAccessRepository
    {
        public List<RoleMenuAccessEntity> SelectList();
        public  Task<List<RoleMenuAccessEntity>> SelectListByRoleIdParentId(RoleMenuAccessEntity roleMenuAccessEntity);
        public  Task<int> Bulk(RoleEntity roleEntity);
    }
}
