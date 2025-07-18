using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity.Client.RecentItem
{
    public class RecentItemEntity
    {
        public int Id { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public long UserId { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;

    }

    public class RecentItemGridEntity
    {
        public List<RecentItemEntity> RecentItems { get; set; } = new List<RecentItemEntity>();
        public int TotalRecords { get; set; } = 0;
    }


    public class RecentItemParameterEntity: PagingSortingEntity
    {
        public int ProductId { get; set; } = 0;
        public long UserId { get; set; } = 0;
    }

}
