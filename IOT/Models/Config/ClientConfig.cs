using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IOT.Models.Config
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasMany(x => x.clientRecords).WithOne(x => x.Client).HasForeignKey(x => x.RFID);
        }
    }
}
