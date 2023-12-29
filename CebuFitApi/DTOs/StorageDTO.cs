using CebuFitApi.Models;

namespace CebuFitApi.DTOs
{
    public class StorageDTO
    {
        public Guid Id { get; set; }
        public List<StorageItem> StorageItems { get; set; }
    }
}
