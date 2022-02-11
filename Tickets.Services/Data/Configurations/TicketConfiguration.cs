﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using Tickets.Services;
using Tickets.Services.Data;

namespace Tickets.Services.Data.Configurations
{
    public partial class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> entity)
        {
            entity.Property(e => e.TicketId).ValueGeneratedNever();

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.Property(e => e.DateUpdated).HasColumnType("datetime");

            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.Property(e => e.StatusCode)
                .IsRequired()
                .HasMaxLength(1)
                .IsFixedLength();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Tickets_Employees");

            entity.HasOne(d => d.Project)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Projects");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany()
                .HasForeignKey(d => d.StatusCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Status");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Ticket> entity);
    }
}
