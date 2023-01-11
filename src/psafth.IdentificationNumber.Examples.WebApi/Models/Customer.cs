using psafth.IdentificationNumber.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace psafth.IdentificationNumber.Examples.WebApi.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        public IIdentificationNumber IdentificationNumber { get; set; }

        public CustomerType Type { get; set; }

        public string Name { get; set; }
    }
}
