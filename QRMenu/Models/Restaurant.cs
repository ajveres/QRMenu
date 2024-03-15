using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRMenu.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = "";

        public byte StateId { get; set; }
        [ForeignKey("StateId")]
        public State? State { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }

        [Phone]
        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Phone { get; set; } = "";

        [StringLength(5, MinimumLength = 5)]
        [Column(TypeName = "char(5)")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; } = "";

        [StringLength(200, MinimumLength = 5)]
        [Column(TypeName = "nvarchar(200)")]
        public string Address { get; set; } = "";

        [Column(TypeName = "smalldatetime")]
        public DateTime RegisterDate { get; set; }

        public virtual List<User>? Users { get; set; } 

        


    }
}