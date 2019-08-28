using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Heavy.Web.ViewModels
{
    public class ManageClaimsViewModel
    {
        [Required]
        public string ClaimId { get; set; }

        [Required]
        public string UserId { get; set; }

        public  List<string> AvailableClaims { get; set; }
    }
}