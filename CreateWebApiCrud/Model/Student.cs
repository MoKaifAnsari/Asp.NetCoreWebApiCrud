using System.ComponentModel.DataAnnotations;

namespace CreateWebApiCrud.Model
{
    public class Student
    {
        public int sid { get; set; }
        public string fname { get; set; } = "";
        public string lname { get; set; } = "";
        public string username { get; set; } = "";
        public string city { get; set; } = "";
        public string state { get; set; } = "";
        public string zip { get; set; } = "";
    }
}
