//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Applicant_Professions
    {
        public int Applicant_ProfessionsId { get; set; }
        public string ApplicantId { get; set; }
        public string Profession { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
    
        public virtual Applicant Applicant { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}
