using CommonLibrary;
using DocumentDBClient;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Client.Review;
using ECommerce.Repository.Client.Review;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Client.Review
{
    public class ReviewMongoBusiness : CommonBusiness, IReviewRepository
    {
        IDocument<ReviewMongoEntity> document;

        public ReviewMongoBusiness(IConfiguration config) : base(config)
        {
            document = CreateDocumentInstance<ReviewMongoEntity>("review");

        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(ReviewEntity reviewEntity)
        {
            if (reviewEntity.Id == 0)
                reviewEntity.Id = new Random().Next(1, int.MaxValue);  

            await document.InsertAsync(reviewEntity.ToMongoEntity());
            return reviewEntity.Id;
        }



        public Task<int> Update(ReviewEntity reviewEntity)
        {
            throw new NotImplementedException();
        }

        Task<ReviewGridEntity> IReviewRepository.SelectForGrid(ReviewParameterEntity reviewParameterEntity)
        {
            throw new NotImplementedException();
        }
    }
}
