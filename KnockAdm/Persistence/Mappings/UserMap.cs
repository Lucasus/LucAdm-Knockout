using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KnockAdm
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(t => t.Id);

            //Fields  
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.UserName).IsRequired().HasMaxLength(25);
            Property(t => t.Email).IsRequired();

            //table  
            ToTable("User");
        }
    }
}