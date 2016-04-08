using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apply.Models {
    public class PersonalInfoViewModel {
        public Applicant Applicant { get; set; }
        public SelectList Salutations { get; set; }
    }
}