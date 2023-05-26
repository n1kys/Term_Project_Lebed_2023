using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Project_Lebed_2023
{
    public class AdvertisementData
    {
        public string SubjectName { get; set; }
        public string AdvertisementType { get; set; }
        public string AdvertisementInfo { get; set; }
        public int ContractNumber { get; set; }
        public string ContractNumberValue { get; set; }
        public int PermitNumber { get; set; }
        public string PermitNumberValue { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime PermitDate { get; set; }
        public int ContinuationNumber { get; set; }
        public DateTime ContinuationDate { get; set; }
        public string Contract { get; set; }
        public string Permit { get; set; }

        public AdvertisementData()
        {
        }
    }

}
