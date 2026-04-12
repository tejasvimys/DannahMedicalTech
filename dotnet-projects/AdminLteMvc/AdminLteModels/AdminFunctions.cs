using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminLteModels
{
    public class AdminFunctions
    {
        public int Employeeid { get; set; }
        [Required]
        public string Userid { get; set; }
         [Required]
        public string Password { get; set; }
        [Required]
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        [Required]
        public string Lastname { get; set; }
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string Telephone { get; set; }
        [Required(ErrorMessage = "The Mobile No is required")]
        public string Mobileno { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Emailaddress { get; set; }
        public bool IsAdmin { get; set; }
        public bool? IsSuperAdmin { get; set; }
        public bool? Isactive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsFirstLogin { get; set; }

    }

    public class LoginModel
    {
        [Required]
        public string Userid { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class ResetPassword
    {
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords Do not match!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Emailaddress { get; set; }

        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }
    }
    public class Roles
    {
        public int Roleid { get; set; }
        [Required(ErrorMessage = "The Role Name is Required!")]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool? isActive { get; set; }
        public int? createdBy { get; set; }
        public DateTime? createdDate { get; set; }
    }

    public class UserRoleMapping
    {
        public List<Roles> LstRoles { get; set; } 
    }
}
