using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace BusinessLMSWeb.Models
{
	public class UsersContext : DbContext
	{
		public UsersContext()
			: base("BusinessLMSContext")
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
		[RegularExpression(@"^[\S]*$", ErrorMessage = "The User Name is only alpha numeric, no spaces, numbers or special caracters allowed")]
		[Display(Name = "UserName", ResourceType = typeof(TextResources.Businesslms))]
		public string UserName { get; set; }

		public string ExternalLoginData { get; set; }
	}

	public class LocalPasswordModel
	{
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "OldPassword", ResourceType = typeof(TextResources.Businesslms))]
		public string OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "NewPassword", ResourceType = typeof(TextResources.Businesslms))]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(TextResources.Businesslms))]
		[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}

	public class LoginModel
	{
		[Required]
		[Display(Name = "UserName", ResourceType = typeof(TextResources.Businesslms))]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(TextResources.Businesslms))]
		public string Password { get; set; }

		[Display(Name = "RememberMe", ResourceType = typeof(TextResources.Businesslms))]
		public bool RememberMe { get; set; }
	}

	public class RegisterModel
	{
		[Required]
		[Display(Name = "UserName", ResourceType = typeof(TextResources.Businesslms))]
		public string UserName { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(TextResources.Businesslms))]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(TextResources.Businesslms))]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}

	public class ForgotModel
	{
		[Required]
		[Display(Name = "UserName", ResourceType = typeof(TextResources.Businesslms))]
		public string UserName { get; set; }
	}

	public class ResetModel
	{
		[Required]
		public string token { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(TextResources.Businesslms))]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(TextResources.Businesslms))]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}

	public class ExternalLogin
	{
		public string Provider { get; set; }
		public string ProviderDisplayName { get; set; }
		public string ProviderUserId { get; set; }
	}
}
