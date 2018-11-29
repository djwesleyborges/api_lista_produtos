using System.Collections.Generic;
namespace DevStore.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public override string ToString() // semelhante ao def __str__ do models python
        {
            return this.Title;
        }
    }
}
