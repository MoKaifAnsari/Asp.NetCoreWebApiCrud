using System.ComponentModel.DataAnnotations;

namespace CreateWebApiCrud.Model
{
    public class StudentModel
    {
        [Required]
        public int sid { get; set; }
        [Required]
        public string fname { get; set; } = "";
        [Required]
        public string lname { get; set; } = "";
        [Required]
        public string username { get; set; } = "";
        [Required]
        public string city { get; set; } = "";
        [Required]
        public string state { get; set; } = "";
        [Required]
        public string zip { get; set; } = "";
        
    }
}
