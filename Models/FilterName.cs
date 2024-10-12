namespace Kartochka_studenta.Models
{
    public class FilterName                                  //класс с названиями факульетов, специальностей и тд
    {
        public List<string>? FacultyFilter { get; set; }
        public List<string>? SpecialityFilter { get; set; }
        public List<string>? CourceFilter { get; set; }
        public List<string>? GroupFilter { get; set; }
        public FilterName() {
            FacultyFilter = new List<string>();
            SpecialityFilter = new List<string>();
            CourceFilter = new List<string>();
            GroupFilter = new List<string>();
        }
        public void updateName(string student, List<string> filter)             //  добавление в лист, если нет совпадений
        {
            if (filter == null)
            {                
                return;
            }
            if (filter.Count == 0)
            {
                filter.Add(student);
                return;
            }
            for (int i = 0; i < filter.Count - 1; i++)
            {
                if (student.ToUpper() == filter[i].ToUpper())
                {
                    return;
                }                 
            }
            if (student.ToUpper() != filter[filter.Count - 1].ToUpper()) 
            {
                filter.Add(student);
            }  
        }
    }
}
