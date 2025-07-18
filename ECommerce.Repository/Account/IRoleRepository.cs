using ECommerce.Entity.Account;
using System.Data;

namespace ECommerce.Repository.Account
{
    public interface IRoleRepository : IBusinessPartial<RoleEntity, RoleMainEntity, RoleGridEntity, RoleParameterEntity, int>
    {
        public List<RoleEntity> SelectList();
    }
}
