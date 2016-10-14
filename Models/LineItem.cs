using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//JOIN TABLE: joins things in a many: many relationship
namespace Bangazon.Models
{
  public class LineItem
  {
    [Key]
    public int LineItemId {get;set;}

    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
  }
}