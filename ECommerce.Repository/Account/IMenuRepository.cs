using ECommerce.Entity.Account;
using System.Data;

namespace ECommerce.Repository.Account
{
    public interface IMenuRepository : IBusiness<MenuEntity, MenuMainEntity, MenuAddEntity, MenuEditEntity, MenuGridEntity, MenuListEntity, MenuParameterEntity, int>
    {
        public  Task<List<MenuEntity>> SelectParent();
        public  Task<List<MenuEntity>> SelectChild(int ParentId);
        public List<MenuEntity> SelectList(MenuEntity menuEntity);

    }
}
