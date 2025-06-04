using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public partial class Patients
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
