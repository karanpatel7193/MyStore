using ECommerce.Business.Client.Home;
using ECommerce.Repository.Client.Home;
using ECommerce.Repository.Client.Review;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Client.Review
{
    public class ReviewFactoryBusiness
    {
        public static IReviewRepository GetInstance(bool isMongoUsed, IConfiguration config)
        {
            if (isMongoUsed)
                return new ReviewMongoBusiness(config);
            else
                return new ReviewSqlBusiness(config);
        }
    }
}
