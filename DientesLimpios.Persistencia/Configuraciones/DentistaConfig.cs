using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DientesLimpios.Persistencia.Configuraciones
{
    public class DentistaConfig : IEntityTypeConfiguration<Dentista>
    {
        public void Configure(EntityTypeBuilder<Dentista> builder)
        {
            builder.HasKey(d => d.Id);

            builder.ComplexProperty(d => d.Email, accion =>
            {
                accion.Property(e => e.Valor)
                    .HasColumnName("Email")
                    .HasMaxLength(254);
            });
        }
    }
}
