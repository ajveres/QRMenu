using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRMenu.Models
{
    public class State
    {
        [Required]                                                              //Required frontendi ilgilendiren bişey.
        [Key]                                                                   // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.None)]                       // değerleri kendimiz gireriz.
        public byte Id { get; set; }
        [Required]                                                              //kişi boş entera basamaz, veri girmeli
        [StringLength(10)]                                                      //Max 10 veri girilmesini sağlar.
        [Column(TypeName = "nvarchar(10)")]
        public string Name { get; set; } = "";

    }
}
