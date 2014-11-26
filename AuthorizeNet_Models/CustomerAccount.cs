using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AuthorizeNet_Models
{
    public class CustomerAccount
    {
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "{0} is not in proper format")]
        [StringLength(100, ErrorMessage = "{0} should be less than {1}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Details")]
        [StringLength(50, ErrorMessage = "{0} should be less than {1}")]
        public string Details { get; set; }
    }
}
