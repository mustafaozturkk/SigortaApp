using System.ComponentModel.DataAnnotations;

namespace SigortaApp.Web.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name ="Ad Soyad")]
        [Required(ErrorMessage ="Lütfen Ad Soyad Giriniz")]
        public string NameSurname { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen Şifre Giriniz")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [Compare("Password",ErrorMessage = "Şifreler Uyuşmuyor!")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Mail Adresi")]
        [Required(ErrorMessage = "Lütfen Mail Giriniz")]
        public string Mail { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Giriniz")]
        public string UserName { get; set; }

        [Display(Name ="Cep Telefonu")]
        [Required(ErrorMessage ="Lütfen Cep Telefonu Numaranızı Giriniz")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Kayıt Tipi")]
        [Required(ErrorMessage = "Lütfen Personel veya Şirket Olduğunuzu Seçiniz")]
        public int IsPersonType { get; set; }
    }
}
