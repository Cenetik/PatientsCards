using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientsCardsUI
{
    public static class GlobalData
    {
        public static ApplicationDbContext Context { get; set; }
    }
}
