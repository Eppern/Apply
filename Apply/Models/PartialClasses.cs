using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apply.Models
{
    [MetadataType(typeof(WorkExperienceMetadata))]
    public partial class WorkExperience
    {

    }

    [MetadataType(typeof(SkillMetadata))]
    public partial class Skill
    {

    }

    [MetadataType(typeof(LanguageCompetenceMetadata))]
    public partial class LanguageCompetence
    {

    }

    [MetadataType(typeof(EducationMetadata))]
    public partial class Education
    {

    }

    [MetadataType(typeof(SkillLevelMetadata))]
    public partial class SkillLevel
    {

    }

    [MetadataType(typeof(LanguageCompetenceLevelMetadata))]
    public partial class LanguageCompetenceLevel
    {

    }

    [MetadataType(typeof(ApplicantMetadata))]
    public partial class Applicant {

    }
}