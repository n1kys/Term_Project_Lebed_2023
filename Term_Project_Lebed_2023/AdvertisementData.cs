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
        public int PermitNumber { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime PermitDate { get; set; }

        public AdvertisementData()
        {
        }
    }
}
