using System;

namespace DevStore.Domain
{
    public class Product
    {
        public Product()
        {
            this.AcquireDate = DateTime.Now; // Caso não for informado nenhuma data, da data do produto sera DateTime.Now
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime AcquireDate { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public override string ToString() // semelhante ao def __str__ do models python
        {
            return this.Title;
        }
    }
}
