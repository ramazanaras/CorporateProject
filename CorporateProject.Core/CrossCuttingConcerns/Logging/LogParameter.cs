using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateProject.Core.CrossCuttingConcerns.Logging
{
    public class LogParameter
    {
        //ismi Product tipi Product ismide product gibi bilgileri alcaz
        public string Name { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
    }
}
