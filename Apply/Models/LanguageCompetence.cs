//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Apply.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LanguageCompetence
    {
        public int LanguageCompetenceId { get; set; }
        public string LanguageName { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
        public int LanguageCompetenceLevelId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual LanguageCompetenceLevel LanguageCompetenceLevel { get; set; }
    }
}
