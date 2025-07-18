using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Admin.Order.OrderInvoice;
using ECommerce.Entity.Admin.Vendor;
using ECommerce.Repository.Admin.Invoice.OrderInvoice.Invoice;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Admin.Invoice.OrderInvoice.Invoice
{
    public class PurchaseInvoiceBusiness : CommonBusiness, IPurchaseInvoiceRepository
    {
        ISql sql;

        public PurchaseInvoiceBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();

        }
        public PurchaseInvoiceEntity MapData(IDataReader reader)
        {
            PurchaseInvoiceEntity purchaseInvoiceEntity = new PurchaseInvoiceEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        purchaseInvoiceEntity.Id = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "Name":
                        purchaseInvoiceEntity.Name = MyConvert.ToString(reader["Name"]);
                        break;
                    case "OrderNumber":
                        purchaseInvoiceEntity.InvoiceNumber = MyConvert.ToInt(reader["OrderNumber"]);
                        break;
                    case "CreatedBy":
                        purchaseInvoiceEntity.CreatedBy = MyConvert.ToLong(reader["CreatedBy"]);
                        break;
                    case "CreatedOn":
                        purchaseInvoiceEntity.CreatedOn = MyConvert.ToDateTime(reader["CreatedOn"]);
                        break;
                    case "LastUpdatedBy":
                        purchaseInvoiceEntity.LastUpdatedBy = MyConvert.ToInt(reader["LastUpdatedBy"]);
                        break;
                    case "LastUpdatedOn":
                        purchaseInvoiceEntity.LastUpdatedOn = MyConvert.ToDateTime(reader["LastUpdatedOn"]);
                        break;
                    case "VendorId":
                        purchaseInvoiceEntity.VendorId = MyConvert.ToInt(reader["VendorId"]);
                        break;
                    case "TotalQuantity":
                        purchaseInvoiceEntity.TotalQuantity = MyConvert.ToDouble(reader["TotalQuantity"]);
                        break;
                    case "TotalAmount":
                        purchaseInvoiceEntity.TotalAmount = MyConvert.ToDouble(reader["TotalAmount"]);
                        break;
                    case "TotalDiscount":
                        purchaseInvoiceEntity.TotalDiscount = MyConvert.ToDouble(reader["TotalDiscount"]);
                        break;
                    case "TotalTax":
                        purchaseInvoiceEntity.TotalTax = MyConvert.ToDouble(reader["TotalTax"]);
                        break;
                    case "TotalFinalAmount":
                        purchaseInvoiceEntity.TotalFinalAmount = MyConvert.ToDouble(reader["TotalFinalAmount"]);
                        break;
                    case "PurchaseInvoiceItem":
                        PurchaseInvoiceItemEntity purchaseInvoiceItemEntity = new PurchaseInvoiceItemEntity
                        {
                            Id = MyConvert.ToInt(reader["Id"]),
                            PurchaseInvoiceId = MyConvert.ToInt(reader["PurchaseInvoiceId"]),
                            ProductId = MyConvert.ToInt(reader["ProductId"]),
                            ProductName = MyConvert.ToString(reader["ProductName"]),
                            Quantity = MyConvert.ToInt(reader["Quantity"]),
                            Price = MyConvert.ToDouble(reader["Price"]),
                            Amount = MyConvert.ToDouble(reader["Amount"]),
                            DiscountPercentage = MyConvert.ToDouble(reader["DiscountPercentage"]),
                            Tax = MyConvert.ToDouble(reader["Tax"]),
                            FinalAmount = MyConvert.ToDouble(reader["FinalAmount"]),
                            ExpiryDate = reader["ExpiryDate"] != null ? MyConvert.ToDateTime(reader["ExpiryDate"]) : null,
                        };
                        purchaseInvoiceEntity.PurchaseInvoiceItems.Add(purchaseInvoiceItemEntity);
                        break;
                }
            }
            return purchaseInvoiceEntity;
        }

        public async Task<PurchaseInvoiceEntity> SelectForRecord(int id)
        {
            sql.AddParameter("Id", id);
            return await sql.ExecuteRecordAsync<PurchaseInvoiceEntity>("PurchaseInvoice_SelectForRecord", CommandType.StoredProcedure);
        }

        public async Task Delete(int id)
        {
            sql.AddParameter("Id", id);
            await sql.ExecuteNonQueryAsync("PurchaseInvoice_Delete", CommandType.StoredProcedure);
        }


        public async Task<int> Insert(PurchaseInvoiceEntity purchaseInvoiceEntity)
        {
            sql.AddParameter("InvoiceNumber", purchaseInvoiceEntity.InvoiceNumber);
            sql.AddParameter("CreatedBy", purchaseInvoiceEntity.CreatedBy);
            sql.AddParameter("CreatedOn", DbType.DateTime, ParameterDirection.Input, purchaseInvoiceEntity.CreatedOn);
            sql.AddParameter("VendorId", purchaseInvoiceEntity.VendorId);
            sql.AddParameter("Description", purchaseInvoiceEntity.Description);
            sql.AddParameter("TotalQuantity", purchaseInvoiceEntity.TotalQuantity);
            sql.AddParameter("TotalAmount", purchaseInvoiceEntity.TotalAmount);
            sql.AddParameter("TotalDiscount", purchaseInvoiceEntity.TotalDiscount);
            sql.AddParameter("TotalTax", purchaseInvoiceEntity.TotalTax);
            sql.AddParameter("TotalFinalAmount", purchaseInvoiceEntity.TotalFinalAmount);

            if (purchaseInvoiceEntity.PurchaseInvoiceItems != null && purchaseInvoiceEntity.PurchaseInvoiceItems.Count > 0)
            {
                sql.AddParameter("InvoiceItemsXML", purchaseInvoiceEntity.PurchaseInvoiceItems.ToXML());
            }
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("PurchaseInvoice_Insert", CommandType.StoredProcedure));

        }

        public async Task<int> Update(PurchaseInvoiceEntity purchaseInvoiceEntity)
        {
            sql.AddParameter("Id", purchaseInvoiceEntity.Id);
            sql.AddParameter("CreatedBy", purchaseInvoiceEntity.CreatedBy);
            sql.AddParameter("CreatedOn", DbType.DateTime, ParameterDirection.Input, purchaseInvoiceEntity.CreatedOn);
            sql.AddParameter("InvoiceNumber", purchaseInvoiceEntity.InvoiceNumber);
            sql.AddParameter("LastUpdatedBy", purchaseInvoiceEntity.LastUpdatedBy);
            sql.AddParameter("LastUpdatedOn", DbType.DateTime, ParameterDirection.Input, purchaseInvoiceEntity.LastUpdatedOn);
            sql.AddParameter("VendorId", purchaseInvoiceEntity.VendorId);
            sql.AddParameter("Description", purchaseInvoiceEntity.Description);
            sql.AddParameter("TotalQuantity", purchaseInvoiceEntity.TotalQuantity);
            sql.AddParameter("TotalAmount", purchaseInvoiceEntity.TotalAmount);
            sql.AddParameter("TotalDiscount", purchaseInvoiceEntity.TotalDiscount);
            sql.AddParameter("TotalTax", purchaseInvoiceEntity.TotalTax);
            sql.AddParameter("TotalFinalAmount", purchaseInvoiceEntity.TotalFinalAmount);
            sql.AddParameter("InvoiceItemsXML", purchaseInvoiceEntity.PurchaseInvoiceItems.ToXML());

            return MyConvert.ToInt(await sql.ExecuteScalarAsync("PurchaseInvoice_Update", CommandType.StoredProcedure));
        }


        public async Task<PurchaseInvoiceAddEntity> SelectForAdd(PurchaseInvoiceParameterEntity purchaseInvoiceParameterEntity)
        {
            return await sql.ExecuteResultSetAsync<PurchaseInvoiceAddEntity>("PurchaseInvoice_SelectForAdd", CommandType.StoredProcedure, 2, MapAddEntity);
        }
        public async Task MapAddEntity(int resultSet, PurchaseInvoiceAddEntity addEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    addEntity.Vendors.Add(await sql.MapDataAsync<VendorMainEntity>(reader));
                    break;
                case 1:
                    addEntity.Products.Add(await sql.MapDataAsync<ProductMainEntity>(reader));
                    break;
            }
        }

        public async Task<PurchaseInvoiceEditEntity> SelectForEdit(PurchaseInvoiceParameterEntity purchaseInvoiceParameterEntity)
        {
            if (purchaseInvoiceParameterEntity.Id != 0)
                sql.AddParameter("Id", purchaseInvoiceParameterEntity.Id);
            return await sql.ExecuteResultSetAsync<PurchaseInvoiceEditEntity>("PurchaseInvoice_SelectForEdit", CommandType.StoredProcedure, 3, MapEditEntity);
        }
        public async Task MapEditEntity(int resultSet, PurchaseInvoiceEditEntity purchaseInvoiceEditEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    purchaseInvoiceEditEntity.PurchaseInvoice = await sql.MapDataAsync<PurchaseInvoiceEntity>(reader);
                    break;
                case 1:
                    purchaseInvoiceEditEntity.PurchaseInvoice.PurchaseInvoiceItems.Add(await sql.MapDataAsync<PurchaseInvoiceItemEntity>(reader));
                    break;
                case 2:
                    purchaseInvoiceEditEntity.Vendors.Add(await sql.MapDataAsync<VendorMainEntity>(reader));
                    break;
                case 3:
                    purchaseInvoiceEditEntity.Products.Add(await sql.MapDataAsync<ProductMainEntity>(reader));
                    break;

            }
        }

        public async Task<PurchaseInvoiceGridEntity> SelectForGrid(PurchaseInvoiceParameterEntity  parameterEntity)
        {
            PurchaseInvoiceGridEntity purchaseInvoiceGridEntity = new PurchaseInvoiceGridEntity();

            if (parameterEntity.Description != string.Empty)
                sql.AddParameter("Description", parameterEntity.Description);

            if (parameterEntity.VendorId != 0)
                sql.AddParameter("VendorId", parameterEntity.VendorId);

            if (parameterEntity.TotalQuantity != 0)
                sql.AddParameter("TotalQuantity", parameterEntity.TotalQuantity);

            if (parameterEntity.TotalAmount != 0)
                sql.AddParameter("TotalAmount", parameterEntity.TotalAmount);

            if (parameterEntity.TotalDiscount != 0)
                sql.AddParameter("TotalDiscount", parameterEntity.TotalDiscount);

            if (parameterEntity.TotalTax != 0)
                sql.AddParameter("TotalTax", parameterEntity.TotalTax);

            if (parameterEntity.TotalFinalAmount != 0)
                sql.AddParameter("TotalFinalAmount", parameterEntity.TotalFinalAmount);

            if (parameterEntity.VendorName != string.Empty)
                sql.AddParameter("Description", parameterEntity.VendorName);

            if (parameterEntity.ProductId != 0)
                sql.AddParameter("ProductId", parameterEntity.ProductId);

            if (parameterEntity.ProductName != string.Empty)
                sql.AddParameter("ProductName", parameterEntity.ProductName);

            sql.AddParameter("SortExpression", parameterEntity.SortExpression);
            sql.AddParameter("SortDirection", parameterEntity.SortDirection);
            sql.AddParameter("PageIndex", parameterEntity.PageIndex);
            sql.AddParameter("PageSize", parameterEntity.PageSize);

            return await sql.ExecuteResultSetAsync<PurchaseInvoiceGridEntity>("PurchaseInvoice_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);
        }

        public async Task MapGridEntity(int resultSet, PurchaseInvoiceGridEntity gridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    gridEntity.Invoices.Add(await sql.MapDataAsync<PurchaseInvoiceEntity>(reader));
                    break;
                case 1:
                    gridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        public async Task<PurchaseInvoiceListEntity> SelectForList(PurchaseInvoiceParameterEntity PurchaseInvoiceParameterEntity)
        {

            if (PurchaseInvoiceParameterEntity.ProductId != 0)
                sql.AddParameter("ProductId", PurchaseInvoiceParameterEntity.ProductId);
            if (PurchaseInvoiceParameterEntity.VendorId != 0)
                sql.AddParameter("VendorId", PurchaseInvoiceParameterEntity.VendorId);
            if (PurchaseInvoiceParameterEntity.VendorName != string.Empty)
                sql.AddParameter("Description", PurchaseInvoiceParameterEntity.VendorName);
            if (PurchaseInvoiceParameterEntity.TotalQuantity != 0)
                sql.AddParameter("TotalQuantity", PurchaseInvoiceParameterEntity.TotalQuantity);
            if (PurchaseInvoiceParameterEntity.TotalAmount != 0)
                sql.AddParameter("TotalQuantity", PurchaseInvoiceParameterEntity.TotalAmount);

            sql.AddParameter("SortExpression", PurchaseInvoiceParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", PurchaseInvoiceParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", PurchaseInvoiceParameterEntity.PageIndex);
            sql.AddParameter("PageSize", PurchaseInvoiceParameterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<PurchaseInvoiceListEntity>("PurchaseInvoice_SelectForList", CommandType.StoredProcedure, 4, MapListEntity);
        }
        public async Task MapListEntity(int resultSet, PurchaseInvoiceListEntity purchaseInvoiceListEntity, IDataReader reader)
        {


            switch (resultSet)
            {
                case 0:
                    purchaseInvoiceListEntity.Vendors.Add(await sql.MapDataAsync<VendorMainEntity>(reader));
                    break;
                case 1:
                    purchaseInvoiceListEntity.Products.Add(await sql.MapDataAsync<ProductMainEntity>(reader));
                    break;
                case 2:
                    purchaseInvoiceListEntity.Invoices.Add(await sql.MapDataAsync<PurchaseInvoiceEntity>(reader));
                    break;
                case 3:
                    purchaseInvoiceListEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }


        public Task<List<PurchaseInvoiceMainEntity>> SelectForLOV(PurchaseInvoiceParameterEntity objParameter)
        {
            throw new NotImplementedException();
        }


    }
}
