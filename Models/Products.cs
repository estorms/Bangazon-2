using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class Product
  {
    [Key]
    public int ProductId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)] //the database will generate the value for this dataType

    public DateTime DateCreated {get;set;}

    [Required]
    [StringLength(255)] //setting max length on description, another type of validation
    public string Description { get; set; }

    [Required]
    public double Price { get; set; }
    public ICollection<LineItem> LineItems;
    // public ICollection<CustomerFave> CustomerFavorites;
  }
}