using System.Text.Json.Serialization;

namespace Kartochka_studenta.Models
{
    public class StudentModel
    {        
        public string? FIO { get; set; }        
        public CurriculumModel? Curriculum { get; set; }
        public AddressModel? Address { get; set; }
        public ContactsModel? Contacts {  get; set; }
        public StudentModel()
        {
            Curriculum = new CurriculumModel();
            Address = new AddressModel();
            Contacts = new ContactsModel();            
        }
    }
}
