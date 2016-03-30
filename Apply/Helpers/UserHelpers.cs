﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Apply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Apply.Helpers {
    public static class UserHelpers {

        public static void CreateAspNetRole(string roleName) {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!roleManager.RoleExists(roleName)) {
                var role = new IdentityRole();
                role.Name = roleName;
                roleManager.Create(role);
            }
        }

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
                    var role = new IdentityRole();
                    role.Name = roleName;
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
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
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
                UserManager.AddToRoles(user.Id, roles.ToArray());
            }
            else {
                throw new Exception("Role does not exist");
            }
        }

        /// <summary>
        /// Adds a user's details taken from external login provider account
        /// </summary>
        /// <param name="user"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static ApplicationUser GetUserDetailsFromExternalProvider(ApplicationUser user, ExternalLoginInfo info) {
            if (info.Login.LoginProvider == "Google") {
                user.Forename = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                //var gender = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "gender").Value;
                //var ImageURL = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "image").Value;
                if (user != null)
                    user.Surname = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value;
            }

            else if (info.Login.LoginProvider == "Facebook") {

            }

            return user;
        }

        public static string GetExternalProviderGlyphicon(string provider) {
            switch (provider) {
                case "Google":
                    return "google-plus";
                    break;
                case "Facebook":
                    return "facebook";
                    break;
                case "Twitter":
                    return "twitter";
                    break;
                default:
                    //error (provider prob changed their name), use a generic button
                    return "";
                    break;
            }
        }

        [Flags]
        public enum Roles {
            SuperAdmin = 1,
            Admin = 1 << 1,
            User = 1 << 2,
            InvitedCompany = 1 << 3
        };
    }
}