using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_web.Models
{
    [Table("cart_items")]
    public class CartItem
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("cart_id")]
        public Guid CartId { get; set; }

        // Note: You are linking directly to ProductId for now instead of ProductVariantId
        [Column("product_variant_id")] 
        public Guid ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        // Navigation property
        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}
