namespace Kartochka_studenta.Models
{
    public class StudentViewModel
    {
        public List<StudentModel> studentModel { get; set; }
        public FilterName filterName { get; set; }                  //класс с названиями факульетов, специальностей и тд
        public StudentViewModel() {
            studentModel = new List<StudentModel>();
            filterName = new FilterName();
        }
    }
}
