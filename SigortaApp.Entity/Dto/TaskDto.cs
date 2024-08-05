using SigortaApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Dto
{
    public class TaskDto : BaseEntityDto
    {
        public int OrderCompanyId { get; set; }
        public Company OrderCompany { get; set; }
        public int SendCompanyId { get; set; }
        public Company SendCompany { get; set; }
        public int CompanyWillPayId { get; set; }
        public Company CompanyWillPay { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int CityUstId { get; set; }
        public City CityUst { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime BusDate { get; set; }
        public int? Carrier { get; set; }
        public string CarrierName { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool IkiKatMi { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<TaskHistoryDto> TaskHistorys { get; set; }
        public List<PaymentTask> PaymentTasks { get; set; }
        public double Price { get; set; }
        public bool CariMi { get; set; }
        public string AliciMi { get; set; }
        public string SahisAdSoyad { get; set; }
        public string SahisTel { get; set; }
        public string SahisFiyat { get; set; }
        public bool SahisMi { get; set; }
        public List<Files> files { get; set; }
    }
}
