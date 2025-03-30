using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IOT.Models.Config
{
    public class ClientRecordConfig : IEntityTypeConfiguration<ClientRecord>
    {
        public void Configure(EntityTypeBuilder<ClientRecord> builder)
        {

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasKey(x => x.Id);
        }
    }
}
