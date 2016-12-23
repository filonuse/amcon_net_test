using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Serious.Users.Models
{
    public class LoginViewModel
    {
        #region Constants

        private const string ERROR_EMAIL_IS_EMPTY = "Email cannot be empty.";
        private const string ERROR_EMAIL_LENGTH = "Email may contain up to 50 characters.";
        private const string ERROR_EMAIL_REGEX = "Email has an invalid format.";
        private const string ERROR_PASSWORD_IS_EMPTY = "Password cannot be empty.";
        private const string ERROR_PASSWORD_LENGTH = "Password may contain up to 50 characters.";

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The e-mail.</value>
        [Required(AllowEmptyStrings = false, ErrorMessage = ERROR_EMAIL_IS_EMPTY)]
        [StringLength(Constants.MAX_STRING_LENGTH_NAME, ErrorMessage = ERROR_EMAIL_LENGTH)]
        [EmailAddress(ErrorMessage = ERROR_EMAIL_REGEX)]
        [AllowHtml]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required(AllowEmptyStrings = false, ErrorMessage = ERROR_PASSWORD_IS_EMPTY)]
        [DataType(DataType.Password)]
        [StringLength(Constants.MAX_STRING_LENGTH_PASSWORD, ErrorMessage = ERROR_PASSWORD_LENGTH)]
        [AllowHtml]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether cookie is persistent.
        /// </summary>
        /// <value><c>True</c> if cookie is persistent; otherwise, <c>false</c>.</value>
        [Display(Name = "Remember me")]
        public bool PersistentCookie { get; set; }
        #endregion
    }
}