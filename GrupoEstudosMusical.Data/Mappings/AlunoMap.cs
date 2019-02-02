﻿using GrupoEstudosMusical.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoEstudosMusical.Data.Mappings
{
    public class AlunoMap : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(a => a.DataNascimento)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(a => a.Telefone)
                .HasColumnType("varchar(10)");

            builder.Property(a => a.Celular)
                .HasColumnType("varchar(11)");

            builder.Property(a => a.Rg)
                .HasColumnType("varchar(9)");

            builder.HasIndex(a => a.Rg)
                .IsUnique();

            builder.Property(a => a.Cpf)
                .HasColumnType("varchar(11)");

            builder.HasIndex(a => a.Cpf)
                .IsUnique();

            builder.Property(a => a.Email)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(a => a.Cep)
                .HasColumnType("varchar(8)");

            builder.Property(a => a.Endereco)
                .HasColumnType("varchar(180)")
                .IsRequired();

            builder.Property(a => a.Complemento)
                .HasColumnType("varchar(50)");

            builder.Property(a => a.Bairro)
                .HasColumnType("varchar(60)")
                .IsRequired();

            builder.Property(a => a.Cidade)
                .HasColumnType("varchar(60)")
                .IsRequired();

            builder.Property(a => a.Uf)
                .HasColumnType("varchar(2)")
                .IsRequired();

            builder.Property(a => a.DataCadastro)
                .HasColumnType("date");
        }
    }
}