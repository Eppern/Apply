﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Apply.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ApplyEntities : DbContext
    {
        public ApplyEntities()
            : base("name=ApplyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Applicant_Professions> Applicant_Professions { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<CV> CVs { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<LanguageCompetence> LanguageCompetences { get; set; }
        public virtual DbSet<LanguageCompetenceLevel> LanguageCompetenceLevels { get; set; }
        public virtual DbSet<Salutation> Salutations { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SkillLevel> SkillLevels { get; set; }
        public virtual DbSet<TargetCompany> TargetCompanies { get; set; }
        public virtual DbSet<WorkExperience> WorkExperiences { get; set; }
    }
}
