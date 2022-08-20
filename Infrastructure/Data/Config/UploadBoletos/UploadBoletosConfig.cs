using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config.UploadBoletos;

public class UploadBoletosConfig : IEntityTypeConfiguration<UploadBoleto>
{
    public void Configure(EntityTypeBuilder<UploadBoleto> b)
    {
        b.HasKey(c => c.Id);
        b.Property(c => c.DataOperacao)
            .HasColumnType("DateTime")
            .IsRequired(true);
        b.Property(c => c.CodigoCliente).IsRequired(true);
        b.Property(c => c.TipoOperacao).IsRequired(true);
        b.Property(c => c.IdBolsa).IsRequired(true);
        b.Property(c => c.CodigoAtivo).IsRequired(true);
        b.Property(c => c.Corretora).IsRequired(true);
        b.Property(c => c.Quantidade).IsRequired(true);
        b.Property(c => c.PrecoUnitario)
            .HasColumnType("decimal(6,2)")
            .IsRequired(true);
        b.Ignore(c => c.ValorDescontoOperacao);
        b.Ignore(c => c.ValorFinanceiroOperacao);
        b.Ignore(c => c.StatusBoleto);
        b.Ignore(c => c.Mensagem);

        b.ToTable("UploadBoletos");
    }
}