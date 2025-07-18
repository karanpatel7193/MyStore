using ECommerce.Entity.Admin.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Admin.Globalization
{
    public interface ICityRepository
    {
        public Task<List<CityMainEntity>> SelectForLOV(CityParemeterEntity CityParameterEntity);

    }
}
