namespace reCaptcha.Models;
using System.ComponentModel.DataAnnotations;

public class FormModel
{
    [Required(ErrorMessage = "İsim girilmesi gerekmektedir!!")]
    [Display(Name = "Adınız")]
    public string Name { get; set; }

    [Required(ErrorMessage = "E-Posta adresi girilmesi gerekmektedir!!")]
    [Display(Name = "E-Posta Adresiniz")]
    [EmailAddress(ErrorMessage = "E-Posta formatı uygun değil")]
    public string Email { get; set; }
}

public class CaptchaResponse
{
    public bool Success { get; set; }
    public double Score { get; set; }
}