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
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    [StringLength(255)]
    //special characters are not allowed
    [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage = "Special character should not be entered")]
    public string Description { get; set; }

    [Required]
    [StringLength(55, ErrorMessage="Please shorten the product title to 55 characters")]
    //special characters are not allowed
    [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage = "Special character should not be entered")]
    public string Title { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:C}")]
    //Price cannot exceed $10,000
    [Range(0.00, 100000.01, ErrorMessage ="Price must be $10,000 or less")]
    public double Price { get; set; }

    [Required]
    public int Quantity { get; set; }
    // HN: For the product details view, the product quantity should show products remaining.

    [Required]
    public string UserId {get; set;}

    public string City {get; set;}

    public string ImagePath {get; set;}

    [Required]
    public ApplicationUser User { get; set; }

    [Required]
    [Display(Name="Product Category")]
    public int ProductTypeId { get; set; }
    
    public ProductType ProductType { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; }
  }
}
