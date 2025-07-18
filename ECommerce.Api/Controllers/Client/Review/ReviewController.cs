using CommonLibrary;
using ECommerce.Business.Client.Product;
using ECommerce.Business.Client.Review;
using ECommerce.Entity.Client.Review;
using ECommerce.Repository.Client.Review;
using ECommerce.Repository.Client.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Client.Review
{
    [Route("client/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        IReviewRepository reviewRepository;
        private readonly IConfiguration _config;

        public ReviewController(IReviewRepository reviewRepository, IConfiguration config)
        {
            this.reviewRepository = reviewRepository;
            _config = config;

        }


        [HttpPost]
        [Route("getGridData", Name = "client.review.gridData")]
        public async Task<Response> GetForGrid(ReviewParameterEntity reviewParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await reviewRepository.SelectForGrid(reviewParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("insert", Name = "client.review.insert")]
        public async Task<Response> Insert(ReviewEntity reviewEntity)
        {
            Response response;
            try
            {
                var reviewRepository = ReviewFactoryBusiness.GetInstance(Common.AppSettings.MongoReview, _config);
                response = new Response(await reviewRepository.Insert(reviewEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("update", Name = "client.review.update")]
        public async Task<Response> Update(ReviewEntity reviewEntity)
        {
            Response response;
            try
            {
                response = new Response(await reviewRepository.Update(reviewEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("delete/{Id:int}", Name = "client.review.delete")]
        public async Task<Response> Delete(int Id)
        {
            Response response;
            try
            {
                await reviewRepository.Delete(Id);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
    }
}
