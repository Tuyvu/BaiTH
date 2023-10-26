namespace BaiTH1.Models
{
    public class Course
    {
        public Course() { 
        Enrollments = new HashSet<Enrollment>();
        }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
