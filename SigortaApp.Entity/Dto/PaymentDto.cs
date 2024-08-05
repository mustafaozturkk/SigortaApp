using SigortaApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Dto
{
    public class PaymentDto : BaseEntityDto
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int StatusId { get; set; }
        public int PaymentMethodId { get; set; }
        public int CaseId { get; set; }
        public int? TaskId { get; set; }
        public Concrete.Task Task { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Alınan { get; set; }
        public decimal Borc { get; set; }
        public decimal CariBakiye { get; set; }
        public string StatusName { get; set; }
        public string PaymentMethodName { get; set; }
        public string CurrentGroupName { get; set; }
    }
}
