using WpfDiary.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryWPF.Models.Configurations
{
    public class GroupConfiguration : EntityTypeConfiguration<Group>
    {

        public GroupConfiguration()
        {
            ToTable("dbo.Groups");
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            // aby inkrementacja na id nie byla automatycznie co 1

            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
        }

    }
}
