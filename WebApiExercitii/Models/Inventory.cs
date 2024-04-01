using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiExercitii.Models
{

    public class Inventory
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public Store Store { get; set; }
        public Guid ProductId {  get; set; }
        public Product Product { get; set; }
        public int NrOfProducts {  get; set; }
    }
}
