using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class Customer
  //name of the class is the name of the database table
  {
    [Key] //primary key, will be CustomerId, as defined below//key attribute is automatically required

    public int CustomerId {get;set;}

    //each property on table
    [Required] //property is required
    [DataType(DataType.Date)] //must be of type date
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)] //the database will generate the value for this dataType
    public DateTime DateCreated {get;set;}

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public ICollection<PaymentType> PaymentTypes; //every customer has multiple payment types. This establishes the relationship, does not actually hold the information about the payment types themselves
  }
}