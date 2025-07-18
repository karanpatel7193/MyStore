using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Admin.Homepage;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Admin.Order.OrderInvoice;
using ECommerce.Entity.Admin.Vendor;
using ECommerce.Repository.Admin.Invoice.OrderInvoice;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Admin.Invoice.OrderInvoice
{
    public class PurchaseOrderBusiness : CommonBusiness, IPurchaseOrderRepository
    {
        ISql sql;

        public PurchaseOrderBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }
        public PurchaseOrderEntity MapData(IDataReader reader)
        {
            PurchaseOrderEntity purchaseOrderEntity = new PurchaseOrderEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        purchaseOrderEntity.Id = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "Name":
                        purchaseOrderEntity.Name = MyConvert.ToString(reader["Name"]);
                        break;
                    case "OrderNumber":
                        purchaseOrderEntity.OrderNumber = MyConvert.ToInt(reader["OrderNumber"]);
                        break;
                    case "CreatedBy":
                        purchaseOrderEntity.CreatedBy = MyConvert.ToLong(reader["CreatedBy"]);
                        break;
                    case "CreatedOn":
                        purchaseOrderEntity.CreatedOn = MyConvert.ToDateTime(reader["CreatedOn"]);
                        break;
                    case "LastUpdatedBy":
                        purchaseOrderEntity.LastUpdatedBy = MyConvert.ToInt(reader["LastUpdatedBy"]);
                        break;
                    case "LastUpdatedOn":
                        purchaseOrderEntity.LastUpdatedOn = MyConvert.ToDateTime(reader["LastUpdatedOn"]);
                        break;
                    case "VendorId":
                        purchaseOrderEntity.VendorId = MyConvert.ToInt(reader["VendorId"]);
                        break;
                    case "TotalQuantity":
                        purchaseOrderEntity.TotalQuantity = MyConvert.ToDouble(reader["TotalQuantity"]);
                        break;
                    case "TotalAmount":
                        purchaseOrderEntity.TotalAmount = MyConvert.ToDouble(reader["TotalAmount"]);
                        break;
                    case "TotalDiscount":
                        purchaseOrderEntity.TotalDiscount = MyConvert.ToDouble(reader["TotalDiscount"]);
                        break;
                    case "TotalTax":
                        purchaseOrderEntity.TotalTax = MyConvert.ToDouble(reader["TotalTax"]);
                        break;
                    case "TotalFinalAmount":
                        purchaseOrderEntity.TotalFinalAmount = MyConvert.ToDouble(reader["TotalFinalAmount"]);
                        break;
                    case "PurchaseOrderInvoiceItem":
                        PurchaseOrderItemEntity purchaseOrderItemEntity = new PurchaseOrderItemEntity
                        {
                            Id = MyConvert.ToInt(reader["Id"]),
                            PurchaseOrderId = MyConvert.ToInt(reader["PurchaseOrderId"]),
                            ProductId = MyConvert.ToInt(reader["ProductId"]),
                            ProductName = MyConvert.ToString(reader["ProductName"]),
                            Quantity = MyConvert.ToInt(reader["Quantity"]),
                            Price = MyConvert.ToDouble(reader["Price"]),
                            Amount = MyConvert.ToDouble(reader["Amount"]),
                            DiscountPercentage = MyConvert.ToDouble(reader["DiscountPercentage"]),
                            Tax = MyConvert.ToDouble(reader["Tax"]),
                            FinalAmount = MyConvert.ToDouble(reader["FinalAmount"]),
                            ExpiryDate = MyConvert.ToDateTime(reader["ExpiryDate"]),
                        };
                        purchaseOrderEntity.PurchaseOrderItem.Add(purchaseOrderItemEntity);
                        break;
                }
            }
            return purchaseOrderEntity;
        }
        public async Task<PurchaseOrderEntity> SelectForRecord(int id)
        {
            sql.AddParameter("Id", id);
            return await sql.ExecuteRecordAsync<PurchaseOrderEntity>("PurchaseOrder_SelectForRecord", CommandType.StoredProcedure);
        }
        public async Task Delete(int id)
        {
            sql.AddParameter("Id", id);
            await sql.ExecuteNonQueryAsync("PurchaseOrder_Delete", CommandType.StoredProcedure);
        }
        public async Task<int> Insert(PurchaseOrderEntity purchaseOrderEntity)
        {
            sql.AddParameter("OrderNumber", purchaseOrderEntity.OrderNumber);
            sql.AddParameter("CreatedBy", purchaseOrderEntity.CreatedBy);
            sql.AddParameter("CreatedOn", DbType.DateTime, ParameterDirection.Input, purchaseOrderEntity.CreatedOn);
            sql.AddParameter("VendorId", purchaseOrderEntity.VendorId);
            sql.AddParameter("Description", purchaseOrderEntity.Description);
            sql.AddParameter("TotalQuantity", purchaseOrderEntity.TotalQuantity);
            sql.AddParameter("TotalAmount", purchaseOrderEntity.TotalAmount);
            sql.AddParameter("TotalDiscount", purchaseOrderEntity.TotalDiscount);
            sql.AddParameter("TotalTax", purchaseOrderEntity.TotalTax);
            sql.AddParameter("TotalFinalAmount", purchaseOrderEntity.TotalFinalAmount);

            if (purchaseOrderEntity.PurchaseOrderItem != null && purchaseOrderEntity.PurchaseOrderItem.Count > 0)
            {
                sql.AddParameter("InvoiceItemsXML", purchaseOrderEntity.PurchaseOrderItem.ToXML());
            }
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("PurchaseOrder_Insert", CommandType.StoredProcedure));

        }
        public async Task<int> Update(PurchaseOrderEntity purchaseOrderEntity)
        {
            sql.AddParameter("Id", purchaseOrderEntity.Id);
            sql.AddParameter("OrderNumber", purchaseOrderEntity.OrderNumber);
            sql.AddParameter("CreatedBy", purchaseOrderEntity.CreatedBy);
            sql.AddParameter("CreatedOn", DbType.DateTime, ParameterDirection.Input, purchaseOrderEntity.CreatedOn);
            sql.AddParameter("LastUpdatedBy", purchaseOrderEntity.LastUpdatedBy);
            sql.AddParameter("LastUpdatedOn", DbType.DateTime, ParameterDirection.Input, purchaseOrderEntity.LastUpdatedOn);
            sql.AddParameter("VendorId", purchaseOrderEntity.VendorId);
            sql.AddParameter("Description", purchaseOrderEntity.Description);
            sql.AddParameter("TotalQuantity", purchaseOrderEntity.TotalQuantity);
            sql.AddParameter("TotalAmount", purchaseOrderEntity.TotalAmount);
            sql.AddParameter("TotalDiscount", purchaseOrderEntity.TotalDiscount);
            sql.AddParameter("TotalTax", purchaseOrderEntity.TotalTax);
            sql.AddParameter("TotalFinalAmount", purchaseOrderEntity.TotalFinalAmount);

            if (purchaseOrderEntity.PurchaseOrderItem != null && purchaseOrderEntity.PurchaseOrderItem.Count > 0)
            {
                sql.AddParameter("InvoiceItemsXML", purchaseOrderEntity.PurchaseOrderItem.ToXML());
            }

            return MyConvert.ToInt(await sql.ExecuteScalarAsync("PurchaseOrder_Update", CommandType.StoredProcedure));
        }
        public async Task<PurchaseOrderAddEntity> SelectForAdd(PurchaseOrderParameterEntity purchaseOrderParameterEntity)
        {
            return await sql.ExecuteResultSetAsync<PurchaseOrderAddEntity>("PurchaseOrder_SelectForAdd", CommandType.StoredProcedure, 2, MapAddEntity);
        }
        public async Task MapAddEntity(int resultSet, PurchaseOrderAddEntity addEntity, IDataReader reader)
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
        public async Task<PurchaseOrderEditEntity> SelectForEdit(PurchaseOrderParameterEntity purchaseOrderParameterEntity)
        {
            if (purchaseOrderParameterEntity.Id != 0)
                sql.AddParameter("Id", purchaseOrderParameterEntity.Id);
            return await sql.ExecuteResultSetAsync<PurchaseOrderEditEntity>("PurchaseOrder_SelectForEdit", CommandType.StoredProcedure, 4, MapEditEntity);
        }
        public async Task MapEditEntity(int resultSet, PurchaseOrderEditEntity purchaseOrderEditEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    purchaseOrderEditEntity.PurchaseOrder = await sql.MapDataAsync<PurchaseOrderEntity>(reader);
                    break;
                case 1:
                    purchaseOrderEditEntity.PurchaseOrder.PurchaseOrderItem.Add(await sql.MapDataAsync<PurchaseOrderItemEntity>(reader));
                    break;
                case 2:
                    purchaseOrderEditEntity.Vendors.Add(await sql.MapDataAsync<VendorMainEntity>(reader));
                    break;
                case 3:
                    purchaseOrderEditEntity.Products.Add(await sql.MapDataAsync<ProductMainEntity>(reader));
                    break;
                
            }
        }
        public async Task<PurchaseOrderGridEntity> SelectForGrid(PurchaseOrderParameterEntity parameterEntity)
        {
            PurchaseOrderGridEntity purchaseOrderGridEntity = new PurchaseOrderGridEntity();

            if (parameterEntity.Description != string.Empty)
                sql.AddParameter("Description", parameterEntity.Description);

            if (parameterEntity.VendorId != 0)
                sql.AddParameter("VendorId", parameterEntity.VendorId);

            if (parameterEntity.TotalQuantity  != 0)
                sql.AddParameter("TotalQuantity", parameterEntity.TotalQuantity);

            if (parameterEntity.TotalAmount  != 0)
                sql.AddParameter("TotalAmount", parameterEntity.TotalAmount);

            if (parameterEntity.TotalDiscount  != 0)
                sql.AddParameter("TotalDiscount", parameterEntity.TotalDiscount);

            if (parameterEntity.TotalTax  != 0)
                sql.AddParameter("TotalTax", parameterEntity.TotalTax);

            if (parameterEntity.TotalFinalAmount  != 0)
                sql.AddParameter("TotalFinalAmount", parameterEntity.TotalFinalAmount);

            if (parameterEntity.VendorName != string.Empty)
                sql.AddParameter("Description", parameterEntity.VendorName);

            // Sorting and pagination parameters
            sql.AddParameter("SortExpression", parameterEntity.SortExpression);
            sql.AddParameter("SortDirection", parameterEntity.SortDirection);
            sql.AddParameter("PageIndex", parameterEntity.PageIndex);
            sql.AddParameter("PageSize", parameterEntity.PageSize);

            // Execute the stored procedure and map the result to PurchaseOrderGridEntity
            return await sql.ExecuteResultSetAsync<PurchaseOrderGridEntity>("PurchaseOrder_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);
        }

        public async Task MapGridEntity(int resultSet, PurchaseOrderGridEntity gridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    gridEntity.OrderInvoices.Add(await sql.MapDataAsync<PurchaseOrderEntity>(reader));
                    break;
                case 1:
                    gridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }
        public async Task<PurchaseOrderListEntity> SelectForList(PurchaseOrderParameterEntity PurchaseOrderParameterEntity)
        {

            if (PurchaseOrderParameterEntity.ProductId != 0)
                sql.AddParameter("ProductId", PurchaseOrderParameterEntity.ProductId);
            if (PurchaseOrderParameterEntity.VendorId != 0)
                sql.AddParameter("VendorId", PurchaseOrderParameterEntity.VendorId);
            if (PurchaseOrderParameterEntity.VendorName != string.Empty)
                sql.AddParameter("Description", PurchaseOrderParameterEntity.VendorName);

            sql.AddParameter("SortExpression", PurchaseOrderParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", PurchaseOrderParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", PurchaseOrderParameterEntity.PageIndex);
            sql.AddParameter("PageSize", PurchaseOrderParameterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<PurchaseOrderListEntity>("PurchaseOrder_SelectForList", CommandType.StoredProcedure, 4, MapListEntity);
        }
        public async Task MapListEntity(int resultSet, PurchaseOrderListEntity PurchaseOrderListEntity, IDataReader reader)
        {


                switch (resultSet)
                {
                    case 0:
                    PurchaseOrderListEntity.Vendors.Add(await sql.MapDataAsync<VendorMainEntity>(reader));
                        break;
                    case 1:
                        PurchaseOrderListEntity.Products.Add(await sql.MapDataAsync<ProductMainEntity>(reader));
                        break;
                    case 2:
                        PurchaseOrderListEntity.OrderInvoices.Add(await sql.MapDataAsync<PurchaseOrderEntity>(reader));
                        break;
                    case 3:
                        PurchaseOrderListEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                        break;
                }
            }
        public async Task<List<PurchaseOrderMainEntity>> SelectForLOV(PurchaseOrderParameterEntity parameterEntity)
        {
            return await sql.ExecuteListAsync<PurchaseOrderMainEntity>("PurchaseOrder_SelectForLOV", CommandType.StoredProcedure);
        }
    }
}
