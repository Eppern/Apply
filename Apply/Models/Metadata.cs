using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apply.Models
{
    public class BaseMetadata {
        [Display(Name = "Submitted By")]
        public string CreatedById;

        [Display(Name = "Modified By")]
        public string ModifiedById;

        [Display(Name = "Date Submitted")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreated;

        [Display(Name = "Date Modified")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateModified;
    }

    public class WorkExperienceMetadata : BaseMetadata {
        [Display(Name = "Firmenname")]
        public string CompanyName;

        [Display(Name = "Position")]
        public string PositionHeld;

        [Display(Name = "Beschreibung")]
        public string Notes;

        [Display(Name = "Startmonat")]
        public string MonthStart;

        [Display(Name = "Startjahr")]
        public int YearStart;

        [Display(Name = "Endmonat")]
        public string MonthEnd;

        [Display(Name = "Endjahr")]
        public int YearEnd;
    }

    public class SkillMetadata : BaseMetadata {
        [Display(Name = "Fähigkeit")]
        public string SkillName;

        //[Display(Name = "Fähigkeitenstärke")]
        //public int LevelName;
    }

    public class LanguageCompetenceMetadata : BaseMetadata {
        [Display(Name = "Sprache")]
        public string LanguageName;

        //[Display(Name = "Sprachenfähigkeit")]
        //public string LevelName;
    }

    public class EducationMetadata : BaseMetadata {
        [Display(Name = "Institut")]
        public string InstitutionName;

        [Display(Name = "Beschreibung")]
        public string Notes;

        [Display(Name = "Startmonat")]
        public string MonthStart;

        [Display(Name = "Startjahr")]
        public int YearStart;

        [Display(Name = "Endmonat")]
        public string MonthEnd;

        [Display(Name = "Endjahr")]
        public int YearEnd;
    }

    public class SkillLevelMetadata
    {
        [Display(Name = "Fähigkeitenstärke")]
        public string LevelName;
    }

    public class LanguageCompetenceLevelMetadata
    {
        [Display(Name = "Sprachenfähigkeit")]
        public string LevelName;
    }
}