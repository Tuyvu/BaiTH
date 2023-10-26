using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BaiTH1.Models
{
    public class Student
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Họ tên phải tối thiểu 4 ký tự, tối đa 100 ký tự")]
        [RegularExpression("^[A-Za-z\\s]{4,100}$", ErrorMessage = "Họ tên phải tối thiểu 4 ký tự, tối đa 100 ký tự")]
        [Display(Name = "Họ tên")]
        public string? Name { get; set; } //Họ tên

        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        [RegularExpression(@"\b[A-Za-z0-9._%+-]+@gmail\.com\b", ErrorMessage = "Địa chỉ email phải có đuôi gmail.com")]
        [Display(Name ="Nhập email")]
        public string? Email { get; set; } //Email

        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+]).{8,}$", ErrorMessage = "Mật khẩu từ 8 ký tự trở lên, có ký tự viết hoa, viết thường, chữ số và ký tự đặc biệt")]
        public string? Password { get; set; }//Mật khẩu

        [Display(Name = "Ngành")]
        [Required(ErrorMessage = "Nganh bắt buộc phải được nhập")]
        public Branch? Branch { get; set; }//Ngành học

        [Required(ErrorMessage = "Giới tính bắt buộc phải được nhập")]
        [Display(Name = "Giới tính")]
        
        public Gender? Gender { get; set; }//Giới tính

        [Required(ErrorMessage = "Hệ bắt buộc phải được nhập")]
        [Display(Name = "Hệ")]
        public bool IsRegular { get; set; }//Hệ: true-chính qui, false-phi cq

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Địa chỉ bắt buộc phải được nhập")]
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }//Địa chỉ

        [Required(ErrorMessage = "Ngày sinh không hợp lệ")]
        [Range(typeof(DateTime), "1/1/1963", "12/31/2005")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBorth { get; set; }//Ngày sinh


        [Required(ErrorMessage = "Nhập điểm từ 0 đến 10")]
        [RegularExpression("^(10(\\.0{1,2})?|[0-9](\\.\\d{1,2})?)$", ErrorMessage = "kiểu số thực và miền giá trị từ 0.0 đến 10.0")]
        public string? Score { get; set; }
      /*  [Required(ErrorMessage = "Ảnh bắt buộc phải được nhập")]*/
        public StudentImage? StudentImage { get; set; }

      
    }
}
