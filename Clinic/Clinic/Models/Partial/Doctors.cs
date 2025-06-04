using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clinic.Models;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public partial class Doctors
    {
        public string FullName
        {
            get
            {
                return $"{Surname + ' ' + Name + ' ' + Patronymic}";
            }
        }
    }
}
