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
    
    public partial class WorkExperience
    {
        public int WorkExperienceId { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
        public string MonthStart { get; set; }
        public string MonthEnd { get; set; }
        public int YearStart { get; set; }
        public int YearEnd { get; set; }
        public string CompanyName { get; set; }
        public string PositionHeld { get; set; }
        public string Notes { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}
