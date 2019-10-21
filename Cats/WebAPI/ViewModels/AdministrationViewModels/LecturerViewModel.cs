using System.ComponentModel;
using System.Linq;
using Application.Core.UI.HtmlHelpers;
using LMP.Models;
using Microsoft.AspNetCore.Html;

namespace WebAPI.ViewModels.AdministrationViewModels
{
    public class LecturerViewModel : BaseNumberedGridItem
    {
        [DisplayName("Полное имя")]
        public string FullName => $"{LastName} {FirstName} {MiddleName}";

        [DisplayName("Логин")] 
        public string Login { get; set; }

        [DisplayName("Последний вход")]
        public string LastLogin { get; set; }

        [DisplayName("Предметы")]
        public string Subjects { get; set; }

        [DisplayName("Статус")] 
        public string IsActive { get; set; }
        
        private string FirstName { get; set; }

        private string LastName { get; set; }

        private string MiddleName { get; set; }

        [DisplayName("Действие")] 
        public HtmlString HtmlLinks { get; set; }

        public int Id { get; set; }

        public static LecturerViewModel FormLecturers(Lecturer lecturer, string htmlLinks)
        {
            return new LecturerViewModel
            {
                Id = lecturer.Id,
                FirstName = lecturer.FirstName,
                LastName = lecturer.LastName,
                MiddleName = lecturer.MiddleName,
                Login = lecturer.User.UserName,
                HtmlLinks = new HtmlString(htmlLinks),
                IsActive = lecturer.IsActive ? "" : "Удален",
                LastLogin = lecturer.User.LastLogin.HasValue ? lecturer.User.LastLogin.ToString() : "-",
                Subjects = lecturer.SubjectLecturers != null && lecturer.SubjectLecturers.Count > 0 &&
                           lecturer.SubjectLecturers.Any(e => !e.Subject.IsArchive)
                    ? lecturer.SubjectLecturers.Count(e => !e.Subject.IsArchive).ToString()
                    : "-"
            };
        }
    }
}