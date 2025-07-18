using DocumentDBClient;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using CommonLibrary;
using ECommerce.Entity.Admin.Master;

namespace ECommerce.Entity.Admin.Homepage
{
    public class BlockMainEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }

    public class BlockEntity : BlockMainEntity
    {
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public List<BlockProductEntity> BlockProducts { get; set; } = new List<BlockProductEntity>();

    }

    public class BlockGridEntity
    {
        public List<BlockEntity> Blocks { get; set; } = new List<BlockEntity>();
        public int TotalRecords { get; set; } = 0;
    }
  
    public class BlockAddEntity
    {
        public List<ProductMainEntity> Products { get; set; } = new List<ProductMainEntity>();
    }

    public class BlockEditEntity : BlockAddEntity
    {
        public BlockEntity Block { get; set; } = new BlockEntity();
    }

    public class BlockListEntity : BlockGridEntity
    {
    }
    public class BlockParameterEntity: PagingSortingEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}
