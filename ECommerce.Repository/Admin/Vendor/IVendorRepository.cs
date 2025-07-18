using ECommerce.Entity.Admin.Globalization;
using ECommerce.Entity.Admin.Vendor;

namespace ECommerce.Repository.Admin.Vendor
{
    public interface IVendorRepository : IBusiness<VendorEntity, VendorMainEntity, VendorAddEntity, VendorEditEntity, VendorGridEntity, VendorListEntity, VendorParameterEntity, int>
    {
        public Task<List<StateMainEntity>> SelectForStateLOV(VendorParameterEntity vendorParameterEntity);

    }
}
