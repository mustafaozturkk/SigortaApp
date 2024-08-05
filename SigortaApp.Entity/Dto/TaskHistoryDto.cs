using SigortaApp.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.Entity.Dto
{
    public class TaskHistoryDto : BaseEntityDto
    {
        public int? TaskId { get; set; }
        public Concrete.Task Task { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int? Carrier { get; set; }
        public string CarrierName { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PutOnHoldDate { get; set; }
        public DateTime? End_BusDate { get; set; }
        public string End_BusPhone { get; set; }
        public double? End_BusPrice { get; set; }
    }
}
