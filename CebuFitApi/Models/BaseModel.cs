using System.ComponentModel.DataAnnotations;

namespace CebuFitApi.Models
{
    public abstract class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}
