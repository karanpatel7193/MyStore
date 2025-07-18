using ECommerce.Entity.Account;
using ECommerce.Entity.Client.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Client.Review
{
    public interface IReviewRepository
    {
        public Task<ReviewGridEntity> SelectForGrid(ReviewParameterEntity reviewParameterEntity);
        public Task<int> Insert(ReviewEntity reviewEntity);
        public Task<int> Update(ReviewEntity reviewEntity);
        public Task Delete(int id);

    }
}
