using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //tabela
            builder.ToTable("Category");
            //ChavePrimária
            builder.HasKey(x => x.Id);
            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd() //Gera o ID automático
                .UseIdentityColumn(); //Pula de 1 - 1

            builder.Property(x => x.Name) //Nome da Propriedade
                .IsRequired() //Not Null
                .HasColumnName("Name") //Nome da Coluna
                .HasColumnType("NVARCHAR") //Tipo de dado
                .HasMaxLength(80); //Tamnho do campo

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            //Índices
            builder.HasIndex(x => x.Slug, "IS_Category_Sulug")
                .IsUnique();

        }
    }
}
