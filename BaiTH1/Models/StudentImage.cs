using BaiTH1.Models;
using System;

public class StudentImage
{
    public StudentImage()
    { }
    public int Id { get; set; } // Mã ảnh đại diện
    public byte[] ImageData { get; set; } // Dữ liệu ảnh (binary)
    public string ImageMimeType { get; set; } // Định dạng ảnh (ví dụ: image/jpeg)
    public int StudentId { get; set; } // Mã sinh viên liên kết đến ảnh đại diện
    public Student Student { get; set; } // Liên kết đến đối tượng sinh viên
}