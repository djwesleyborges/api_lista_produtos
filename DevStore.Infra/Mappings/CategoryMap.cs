using DevStore.Domain;
using System.Data.Entity.ModelConfiguration;

namespace DevStore.Infra.Mappings
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable("Caterory"); // Para não criar as tabelas no plural

            HasKey(x => x.Id); // a chave dessa tabela e o Id
            Property(x => x.Title).HasMaxLength(60).IsRequired();
        }
    }
}
