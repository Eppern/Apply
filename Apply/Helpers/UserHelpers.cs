using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Apply.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Claims;

namespace Apply.Helpers {
    public static class UserHelpers {

        /// <summary>
        /// Adds an IdentityRole to the AspNetRoles table in the database
        /// </summary>
        /// <param name="roleName">Name of the role to create</param>
        public static void CreateAspNetRole(string roleName) {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            //ensure role doesn't already exist
            if (!roleManager.RoleExists(roleName)) {
                var role = new IdentityRole();
                role.Name = roleName;
                roleManager.Create(role);
            }
        }

        /// <summary>
        /// Creates IdentityRoles for each role in the Roles Enum
        /// </summary>
        public static void CreateAspNetRoles() {

            int numOfRoles = Enum.GetNames(typeof(Roles)).Length;
            string[] roleNames = new string[numOfRoles];
            int i = 0;
            foreach (Roles roleVal in Enum.GetValues(typeof(Roles))) {
                roleNames[i] = roleVal.ToString();
                i++;
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            foreach (string roleName in roleNames) {
                if (!roleManager.RoleExists(roleName)) {
                    var role = new IdentityRole {Name = roleName};
                    roleManager.Create(role);
                }
            }
        }

        /// <summary>
        /// Adds user to a AspNetRole and all other roles that are lower in the Roles Enum hierarchy
        /// </summary>
        /// <param name="user">Application User</param>
        /// <param name="role">Roles enum role</param>
        public static void AddUserToRole(ApplicationUser user, Roles role) {
            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            List<string> roles = new List<string>();

            if (roleManager.RoleExists(role.ToString())) {
                //ascertain level, add to this role and every role beneath it
                int level = (int)role;
                foreach (Roles roleVal in Enum.GetValues(typeof(Roles))) {
                    if ((int)roleVal >= level) {
                        roles.Add(roleVal.ToString());
                    }
                }
                userManager.AddToRoles(user.Id, roles.ToArray());
            }
            else {
                throw new Exception("Role does not exist");
            }
        }

        /// <summary>
        /// Adds a user's details taken from external login provider account
        /// </summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="info">ExternalLoginInfo</param>
        /// <returns></returns>
        public static ApplicationUser GetUserDetailsFromExternalProvider(ApplicationUser user, ExternalLoginInfo info) {
            if (info.Login.LoginProvider == "Google") {
                var forename = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
                if (forename != null)
                    user.Forename = forename.Value;
                var surname = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
                if (surname != null)
                    user.Surname = surname.Value;
                user.UserName = info.ExternalIdentity.GetUserName();
            }

            return user;
        }

        /// <summary>
        /// gets the bootstrap glyphicon class name for the given provider
        /// </summary>
        /// <param name="provider">Social Media logon provider</param>
        /// <returns>string</returns>
        public static string GetExternalProviderGlyphicon(string provider) {
            string result;
            switch (provider) {
                case "Google":
                    result = "google-plus";
                    break;
                case "Facebook":
                    result = "facebook";
                    break;
                case "Twitter":
                    result = "twitter";
                    break;
                default:
                    //error (provider prob changed their name), use a generic button
                    result = "link";
                    break;
            }
            return result;
        }

        /// <summary>
        /// Creates a Company entity
        /// </summary>
        /// <param name="user">ApplicationUser</param>
        /// <param name="model">RegisterViewModel</param>
        public static void CreateCompanyFromIdentity(ApplicationUser user, RegisterViewModel model)
        {
            var company = new Company
            {
                CompanyName = model.CompanyName,
                UserName = user.UserName,
                ContactName = model.ContactName,
                EmailAddress = user.Email,
                Telephone = model.Telephone,
                Website = model.Website,
                AspNetUserId = user.Id
            };

            var db = new ApplyEntities();
            try
            {
                db.Companies.Add(company);
                db.SaveChanges();
            }
            catch (DbUpdateException ex) {
                var errorHelper = new ControllerHelpers();
                errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "Account", "Register");
            }
        }

        /// <summary>
        /// Creates a geek entity
        /// </summary>
        /// <param name="user">ApplicationUser</param>
        public static void CreateApplicantFromIdentity(ApplicationUser user) {
            var applicant = new Applicant
            {
                ApplicantId = user.Id,
                ForeName = user.Forename ?? "",
                SurName = user.Surname ?? "",
                CreatedById = user.Id,
                ModifiedById = user.Id,
                DateCreated = DateTime.Now
            };
            applicant.DateModified = applicant.DateCreated;
            var db = new ApplyEntities();
            try {
                db.Applicants.Add(applicant);
                db.SaveChanges();
            }
            catch (DbUpdateException ex) {
                var errorHelper = new ControllerHelpers();
                errorHelper.CreateErrorPage(ex.InnerException.InnerException.Message, "Account", "Register");
            }
        }

        [Flags]
        public enum Roles {
            SuperAdmin = 1,
            Admin = 1 << 1,
            User = 1 << 2,
            Company = 1 << 3,
            InvitedCompany = 1 << 4
        };

        /// <summary>
        /// Returns a list of abbreviated month names
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMonths()
        {            
            List<string> month = new List<string> ();
            month.Add("Jan");
            month.Add("Feb");
            month.Add("Mär");
            month.Add("Apr");
            month.Add("Mai");
            month.Add("Jun");
            month.Add("Jul");
            month.Add("Aug");
            month.Add("Sep");
            month.Add("Okt");
            month.Add("Nov");
            month.Add("Dez");
            return month;
        }

        /// <summary>
        /// Returns a list of years from 1900 up to the current year
        /// </summary>
        /// <returns></returns>
        public static List<int> GetYears()
        {
            List<int> year = new List<int>();

            for (int i = DateTime.Now.Year; i > 1900 ; i--)
            {
                year.Add(i);
            }
            return year;
        }
    }
}