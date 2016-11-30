using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukeDotNetWPF.Models
{
    public class IndexOverview
    {
        public string IndexName { get; set; }
        public string NumberOfFields { get; set; }
        public string NumberOfDocuments { get; set; }
        public string NumberOfTerms { get; set; }
        public string HasDeletions { get; set; }
        public string IndexVersion { get; set; }
        public string LastModified { get; set; }
    }
}
