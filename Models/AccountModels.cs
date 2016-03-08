using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace WDTAssignment2NWBA.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Customer name cannot be longer than 50 characters.")]
        [Display(Name = "Name")]
        public string CustomerName { get; set; }

        [StringLength(11, MinimumLength = 1, ErrorMessage = "TFN cannot be longer than 11 characters.")]
        public string TFN { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Address cannot be longer than 50 characters.")]
        public string Address { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "City cannot be longer than 40 characters.")]
        public string City { get; set; }

        [StringLength(20, MinimumLength = 1, ErrorMessage = "State cannot be longer than 20 characters.")]
        public string State { get; set; }

        [Display(Name = "Post Code"), StringLength(4, MinimumLength = 4, ErrorMessage = "Must be a 4 digit number"), Range(0000, 9999, ErrorMessage = "Must be a 4 digit number")]
        public string PostCode { get; set; }


        [Required(ErrorMessage = "Phone is required.")]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "Phone cannot be longer than 15 characters.")]
        [RegularExpression(@"^\((61)\)-[0-9]{4} [0-9]{4}$", ErrorMessage = "Invalid Phone Number! Phone must be in the format (61)- XXXX XXXX.")]
        public string Phone { get; set; }

      


    }

    public enum AusStates
    {
        ACT = 1, NSW = 2, NT = 3, QLD = 4, SA = 5, TAS = 6, VIC = 7, WA = 8
    }


    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
