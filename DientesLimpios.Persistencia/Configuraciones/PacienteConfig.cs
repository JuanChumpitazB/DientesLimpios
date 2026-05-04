using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DientesLimpios.Persistencia.Configuraciones
{
    public class PacienteConfig : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(250);

            builder.ComplexProperty(p => p.Email, accion =>
            {
                accion.Property(e => e.Valor).HasColumnName("Email").HasMaxLength(254);
            });
        }
    }
}
