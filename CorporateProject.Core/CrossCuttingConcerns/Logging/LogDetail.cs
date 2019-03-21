using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateProject.Core.CrossCuttingConcerns.Logging
{
    //methodun bilgileri
    public class LogDetail
    {
        public string FullName { get; set; } //namespace
        public string MethodName { get; set; } //hangi method
        public List<LogParameter> Parameters { get; set; } //methodun parametreleri
    }
}
