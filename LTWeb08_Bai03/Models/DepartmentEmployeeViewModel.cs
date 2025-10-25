using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTWeb08_Bai03.Models
{
    public class DepartmentEmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public int? DeptId { get; set; }
        public string DeptName { get; set; }
    }
}