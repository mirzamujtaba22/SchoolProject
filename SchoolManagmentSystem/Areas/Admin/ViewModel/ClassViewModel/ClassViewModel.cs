using System.ComponentModel.DataAnnotations;

namespace SchoolManagment.UI.Areas.Admin.ViewModel.ClassViewModel
{
    public class ClassViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Section { get; set; }
    }
}
