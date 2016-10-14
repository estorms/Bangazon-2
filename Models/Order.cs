using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class Order
  {
    [Key]
    public int OrderId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    
    [DataType(DataType.Date)]
    public DateTime? DateCompleted {get;set;}

    public int CustomerId {get;set;} //putting a property on the order of type Customer(don't think of it as a class, think of it as a database table): this represents the foreign key. Customer PK = CustomerId
    public Customer Customer {get;set;} 

    public int? PaymentTypeId {get;set;} //another FK: WANT TO TALK ABOUT WHY WE NEED BOTH ID AND TYPE (26 and 27)
    public PaymentType PaymentType {get;set;}  

//ICollection<variable>allows compiler to decide what type of collection to implement (queryClass?)
    public ICollection<LineItem> LineItems;
  }
}