﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clinic.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ClinicEntities : DbContext
    {
        public ClinicEntities()
            : base("name=ClinicEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Doctors> Doctors { get; set; }
        public virtual DbSet<DoctorsShedule> DoctorsShedule { get; set; }
        public virtual DbSet<Genders> Genders { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<Plots> Plots { get; set; }
        public virtual DbSet<Reception> Reception { get; set; }
        public virtual DbSet<Specialitys> Specialitys { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
