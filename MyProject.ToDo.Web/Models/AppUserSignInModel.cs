using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ToDo.Web.Models
{
    public class AppUserSignInModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        [Display(Name = "Kullanıcı Adı:")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]  // parolayı gizledik
        [Required(ErrorMessage = "Parola alanı boş geçilemez")]
        [Display(Name = "Parola:")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")] // client side da kullanıcıya gösterilen kısım
        public bool RememberMe { get; set; }
    }
}
