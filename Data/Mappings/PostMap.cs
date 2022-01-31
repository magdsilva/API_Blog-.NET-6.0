using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Blog.Data.Mappings.PostMap;

namespace Blog.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //tabela
            builder.ToTable("Post");
            //ChavePrimária
            builder.HasKey(x => x.Id);
            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd() //Gera o ID automático
                .UseIdentityColumn(); //Pula de 1 - 1

            builder.Property(x => x.LastUpdateDate)
                .IsRequired()
                .HasColumnName("LastUpdateDate")
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValueSql("GETDATE()"); //GETDATE do SQL
                                                  //.HasDefaultValue(DateTime.Now.ToUniversalTime() Caso queira optar pelo DATETIME do .NET


            //Reacionamento 1 para muitos - Post/Author - Post Category
            builder.HasOne(x => x.Author)
                .WithMany(x=> x.Posts)
                .HasConstraintName("FK_Post_Author")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Category)
               .WithMany(x => x.Posts)
               .HasConstraintName("FK_Post_Category")
               .OnDelete(DeleteBehavior.Cascade);

            //Relacionamento Muitos para Muitos

            builder.HasMany(x => x.Tags)
                .WithMany(x=>x.Posts)
                .UsingEntity<Dictionary<string, object>>("PostTag", post => post.HasOne<Tag>()
                .WithMany()
                .HasForeignKey("PostId")
                .HasConstraintName("FK_PostTag_PostID")
                .OnDelete(DeleteBehavior.Cascade),
                tag => tag.HasOne<Post>()
                .WithMany()
                .HasForeignKey("TagId")
                .HasForeignKey("FK_PostTag_TagId")
                .OnDelete(DeleteBehavior.Cascade)
                );


            builder.HasIndex(x => x.Slug, "IX_Post_Slug")
                .IsUnique();


        }
    }


}
