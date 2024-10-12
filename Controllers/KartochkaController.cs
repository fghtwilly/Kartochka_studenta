using Kartochka_studenta.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Kartochka_studenta.Controllers
{
    public class KartochkaController : Controller
    {
        public string path = "C:\\Users\\Михаил\\ПАПКА\\IT\\C#\\aspnet\\Kartochka_studenta\\Карточки студентов";
        public IActionResult Index()
        {
            /*List<StudentViewModel> ViewModel = new List<StudentViewModel>();
            string[] fileName = Directory.GetFiles("C:\\Users\\Михаил\\ПАПКА\\IT\\C#\\aspnet\\Kartochka_studenta\\Карточки студентов");
            foreach(string item in fileName)
            {                
                StreamReader sr = new StreamReader(item);
                string? jsonToText = sr.ReadToEnd();
                StudentViewModel? student = JsonSerializer.Deserialize<StudentViewModel>(jsonToText);
                ViewModel.Add(student);                
            } */

            
             StudentModel? student = new StudentModel();
             StudentViewModel ViewModel = new StudentViewModel();
             string[] fileName = Directory.GetFiles(path);
             foreach (string item in fileName)
             {
                StreamReader sr = new StreamReader(item);
                string? jsonToText = sr.ReadToEnd();
                student = JsonSerializer.Deserialize<StudentModel>(jsonToText);
                ViewModel.studentModel.Add(student); 
                ViewModel.filterName.updateName(student.Curriculum.Faculty, ViewModel.filterName.FacultyFilter);           // поиск уникальных Faculty, Speciality и т.д. для select
                ViewModel.filterName.updateName(student.Curriculum.Speciality, ViewModel.filterName.SpecialityFilter);
                ViewModel.filterName.updateName(student.Curriculum.Cource, ViewModel.filterName.CourceFilter);
                ViewModel.filterName.updateName(student.Curriculum.Group, ViewModel.filterName.GroupFilter);
                sr.Close();                                                                                             //?
             }
             return View(ViewModel);
            
        }
        [HttpPost]
        public IActionResult KartPartial(string? FIOFind, string? FacultyFind, string? SpecialityFind)
        {
            StudentModel ViewModel = new StudentModel();
            if (FIOFind == null)
            {                
                return PartialView("_KartPartial", ViewModel);
            }
            StudentModel? student = new StudentModel();
            string[] fileName = Directory.GetFiles(path);
            foreach (string item in fileName)
            {
                StreamReader sr = new StreamReader(item);
                string? jsonToText = sr.ReadToEnd();
                student = JsonSerializer.Deserialize<StudentModel>(jsonToText);
                if (student.FIO == FIOFind & student.Curriculum.Faculty == FacultyFind & student.Curriculum.Speciality == SpecialityFind) {
                    sr.Close();
                    return PartialView("_KartPartial", student);                    
                }                
            }
            return Json(new { success = true });           
        }

        [HttpPost]
        public IActionResult SaveNewKart(StudentModel model) {            
            var options1 = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string pathNew = path+ "\\" + model.FIO + "_"+ model.Curriculum.Faculty + ".txt";
            string textToJson = JsonSerializer.Serialize(model, options1);
            FileInfo newFile = new FileInfo(pathNew);
            StreamWriter sw = newFile.CreateText();
            sw.Write(textToJson);
            sw.Close();            
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult SelectPartial(string? filterFaculty, string? filterSpeciality, string? filterCource, string? filterGroup)
        {
            StudentViewModel ViewModel = new StudentViewModel();
            string[] fileName = Directory.GetFiles(path);
            foreach (string item in fileName)
            {
                StreamReader sr = new StreamReader(item);
                string? jsonToText = sr.ReadToEnd();
                StudentModel? student = JsonSerializer.Deserialize<StudentModel>(jsonToText);
                if ((student.Curriculum.Faculty.ToUpper() == filterFaculty.ToUpper() || filterFaculty == "nonfilter") & (student.Curriculum.Speciality.ToUpper() == filterSpeciality.ToUpper() || filterSpeciality == "nonfilter"))
                {
                    if ((student.Curriculum.Cource.ToUpper() == filterCource.ToUpper() || filterCource == "nonfilter") & (student.Curriculum.Group.ToUpper() == filterGroup.ToUpper() || filterGroup == "nonfilter"))
                    {
                        ViewModel.studentModel.Add(student);
                    }
                }
                sr.Close();
            }
            return PartialView("_SelectPartial", ViewModel);
        }        
    }
}
