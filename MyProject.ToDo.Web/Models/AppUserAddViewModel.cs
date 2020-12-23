using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ToDo.Web.Models
{
    public class AppUserAddViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        [Display(Name= "Kullanıcı Adı:")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Parola alanı boş geçilemez")]
        [Display(Name = "Parola:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Parolanızı tekrar girin")]
        [Compare("Password", ErrorMessage = "Parolalar eşleşmiyor")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email :")]
        [EmailAddress(ErrorMessage = "Geçersiz email")]
        [Required(ErrorMessage = "Email alanı boş geçilemez")]
        public string Email { get; set; }

        [Display(Name = "Adınız :")]
        [Required(ErrorMessage = "Ad alanı boş geçilemez")]
        public string Name { get; set; }


        [Display(Name = "Soyadınız :")]
        [Required(ErrorMessage = "Soyad alanı boş geçilemez")]
        public string Surname { get; set; }

    }
}
