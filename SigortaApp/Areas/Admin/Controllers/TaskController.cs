using AspNetCoreHero.ToastNotification.Abstractions;
using SigortaApp.BL;
using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity;
using SigortaApp.Entity.Concrete;
using SigortaApp.Entity.Dto;
using SigortaApp.Entity.Enums;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.EntityFramework;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Enums = SigortaApp.Entity.Enums;

namespace SigortaApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaskController : BaseController<TaskDto>
    {
        private readonly ITaskService _taskService;
        private readonly INotyfService _notyfService;
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;
        private readonly ICityService _cityService;
        private readonly ITaskHistoryService _taskHistoryService;
        private readonly IPaymentTaskService _paymentTaskService;
        private readonly IFilesService _filesService;
        private readonly IFileTaskService _fileTaskService;
        private readonly Context _context;
        private readonly IPaymentService _paymentService;
        private readonly IUnitService _unitService;
        private readonly IUserRoleService _userRoleService;
        private readonly UserManager<AppUser> _userManager;

        public TaskController(ITaskService taskService, INotyfService notyfService, Context context, ICompanyService companyService, IUserService userService, ICityService cityService, ITaskHistoryService taskHistoryService, IPaymentTaskService paymentTaskService, IFilesService filesService, IFileTaskService fileTaskService, IPaymentService paymentService, IUnitService unitService, IUserRoleService userRoleService, UserManager<AppUser> userManager)
        {
            _taskService = taskService;
            _notyfService = notyfService;
            _context = context;
            _companyService = companyService;
            _userService = userService;
            _cityService = cityService;
            _taskHistoryService = taskHistoryService;
            _paymentTaskService = paymentTaskService;
            _filesService = filesService;
            _fileTaskService = fileTaskService;
            _paymentService = paymentService;
            _unitService = unitService;
            _userRoleService = userRoleService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin,Kullanici,Kurye,Otogar")]
        public IActionResult Index()
        {
            //Whatsapp();
            GetCompanysAndCurrier();
            return View();
        }

        public IActionResult GetData()
        {
            var taskList = _taskService.GetListWithCompanyCityAndStatus();
            if (User.Claims.Where(w => w.Type.Contains("role")).First().Value == Enums.RoleEnum.Admin.ToString())
            {
                //taskList = _taskService.GetListWithCompanyCityAndStatus();
            }
            else if (User.Claims.Where(w => w.Type.Contains("role")).First().Value == Enums.RoleEnum.Otogar.ToString())
            {
                taskList = taskList.Where(w => w.StatusId == (int)Enums.StatusEnum.OtogaraYonlendirildi).ToList();
            }
            else if (User.Claims.Where(w => w.Type.Contains("role")).First().Value == Enums.RoleEnum.Kullanici.ToString())
            {
                taskList = taskList.Where(w => w.CreatedBy == Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).ToList();
            }
            else
            {
                taskList = taskList.Where(w => w.Carrier == Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value) || (w.StatusId == (int)Enums.StatusEnum.IsOlusturuldu || w.StatusId == (int)Enums.StatusEnum.KuryeGeriYonlendirdi && w.Carrier == null)).ToList();
            }
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            if (searchValue != null)
            {
                taskList = taskList.Where(w => w.OrderCompany.Name.ToLower().Contains(searchValue.ToLower())
                            || w.SendCompany.Name.ToLower().Contains(searchValue.ToLower())
                            || w.City.Name.ToLower().Contains(searchValue.ToLower())
                            || w.Id.ToString().Contains(searchValue.ToLower())
                            || w.CarrierName.ToLower().Contains(searchValue.ToLower())).ToList();
            }
            var result = LoadData(taskList);

            return Json(result.Value);
        }


        public async void Whatsapp(Task task, WhatsappEnum whatsappEnum, string SahisFiyat)
        {
            var gidenSirket = SahisFiyat != null && SahisFiyat != "" ? _companyService.TGetById(task.SendCompanyId) : _companyService.TGetById(task.OrderCompanyId);
            var users = _userService.GetList().ToList();
            var roleusers = _userManager.GetUsersInRoleAsync("Kurye").Result.ToList();
            var url = "https://api.ultramsg.com/instance72775/messages/chat";
            var client = new RestClient();
            var request = new RestRequest();
            if ((int)whatsappEnum == (int)WhatsappEnum.IsOlusturma)
            {
                foreach (var item in roleusers)
                {
                    client = new RestClient(url);
                    request = new RestRequest(url, Method.Post);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddParameter("token", "m6kw882zxd4or0um");
                    request.AddParameter("to", "+9" + item.PhoneNumber);
                    request.AddParameter("body", task.Id + " Nolu iş oluşturuldu. Yakınsanız işi üzerinize alınız.");
                    RestResponse response = await client.ExecuteAsync(request);
                    var output = response.Content;
                }

                if (gidenSirket.CurrentGroupId == 15)
                {
                    client = new RestClient(url);
                    request = new RestRequest(url, Method.Post);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddParameter("token", "m6kw882zxd4or0um");
                    request.AddParameter("to", "+9" + gidenSirket.MobilePhone);
                    request.AddParameter("body", "Siparişiniz otobüse teslim edilmek üzere teslim alınmıştır. Lütfen " + SahisFiyat + " TL tutarındaki ödemenizi \nTR67 0011 1000 0000 0102 2302 21 \nQNB Bankası \nCemil Canpolat numaralı IBAN'a havale ederek dekont paylaşınız.");
                    RestResponse response = await client.ExecuteAsync(request);
                    var output = response.Content;
                }

            }
            else if ((int)whatsappEnum == (int)WhatsappEnum.IsBitirildi)
            {
                url = "https://api.ultramsg.com/instance72775/messages/image";
                client = new RestClient(url);
                request = new RestRequest(url, Method.Post);
                var busPhone = _taskHistoryService.GetList().Where(w => w.TaskId == task.Id && w.StatusId == (int)StatusEnum.IsBitirildi).Select(s => s.End_BusPhone).FirstOrDefault();
                var filetask = _fileTaskService.GetFileTaskByTaskId(task.Id);
                var file = _filesService.GetList().Where(w => filetask.Select(s => s.FilesId).Contains(w.Id)).OrderBy(o => o.Id).FirstOrDefault();
                if (file != null)
                {
                    request.AddParameter("token", "m6kw882zxd4or0um");
                    request.AddParameter("to", "+9" + gidenSirket.MobilePhone);
                    request.AddParameter("body", "");
                    request.AddParameter("image", Convert.ToBase64String(file.DataFiles));
                    request.AddParameter("caption", "Sayın " + gidenSirket.Name + ", \nİş No: #" + task.Id + " İstediğiniz parça/parçalar teslim edilmek üzere otobüse verildi.\nOtobüs Telefon No: " + busPhone ?? "Yetkiliyle İletişime Geçiniz");

                    RestResponse response = await client.ExecuteAsync(request);
                    var output = response.Content;
                }

            }
            //else if ((int)whatsappEnum == (int)WhatsappEnum.OtogaraYonlendirildi)
            //{
            //    client = new RestClient(url);
            //    request = new RestRequest(url, Method.Post);
            //    request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //    request.AddParameter("token", "m6kw882zxd4or0um");
            //    request.AddParameter("to", "+9" + gidenSirket.MobilePhone);
            //    request.AddParameter("body", "Sayın " + gidenSirket.Name + ", \n#" + task.Id + " Nolu iş Otogara Gönderildi. En kısa sürede Otobüse teslim edilecektir.");
            //    RestResponse response = await client.ExecuteAsync(request);
            //    var output = response.Content;
            //}
            // //var file = _filesService.TGetById(1);
            // //var base64 = Convert.ToBase64String(file.DataFiles);
            // url = "https://api.ultramsg.com/instance44652/messages/chat";
            // //var url = "https://api.ultramsg.com/instance44652/messages/image";
            // client = new RestClient(url);

            // request = new RestRequest(url, Method.Post);
            // request.AddHeader("content-type", "application/x-www-form-urlencoded");
            // request.AddParameter("token", "mfc3gnrmhkxakwul");
            // request.AddParameter("to", "+9" + gidenSirket.MobilePhone);
            // request.AddParameter("body", "İstediğiniz parça/parçalar alınmak üzere sistemimize kayıt edildi.");
            //request.AddParameter("image", Convert.ToBase64String(file.DataFiles));
            //request.AddParameter("caption", "image Caption");


            // RestResponse response = await client.ExecuteAsync(request);
            // var output = response.Content;
        }

        [Authorize(Roles = "Admin,Kullanici")]
        public IActionResult AddTask(TaskDto taskDto)
        {
            if (taskDto.CompanyWillPayId == 1)
            {
                taskDto.CompanyWillPayId = taskDto.SendCompanyId;
            }
            else
            {
                taskDto.CompanyWillPayId = taskDto.OrderCompanyId;
            }
            //todo
            //İki kat için işlem yapılacak.
            Task t = new Task
            {
                Id = taskDto.Id,
                BusDate = taskDto.BusDate,
                Carrier = null,
                CityId = taskDto.CityId,
                CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                CreatedDate = DateTime.Now,
                IsActive = true,
                Description = taskDto.Description,
                OrderCompanyId = taskDto.OrderCompanyId,
                SendCompanyId = taskDto.SendCompanyId,
                StatusId = User.Claims.Where(w => w.Type.Contains("role")).First().Value == Enums.RoleEnum.Kullanici.ToString() ? (int)Enums.StatusEnum.IsIletildi : (int)Enums.StatusEnum.IsOlusturuldu,
                CompanyWillPayId = taskDto.CompanyWillPayId
            };
            if (taskDto.Id != 0)
            {
                t.StatusId = (int)Enums.StatusEnum.Duzenlendi;
                _taskService.TUpdate(t);
            }
            else
            {
                if (taskDto.SahisMi)
                {
                    Company company = new Company
                    {
                        Id = 0,
                        Name = taskDto.SahisAdSoyad,
                        CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                        CreatedDate = DateTime.Now,
                        IsActive = false,
                        MobilePhone = taskDto.SahisTel,
                        Phone = taskDto.SahisTel,
                        CurrentGroupId = 15,
                        Title = taskDto.SahisAdSoyad,
                    };
                    _companyService.TAdd(company);

                    t.SendCompanyId = company.Id;
                    t.CompanyWillPayId = company.Id;
                }
                _taskService.TAdd(t);
                Whatsapp(t, WhatsappEnum.IsOlusturma ,taskDto.SahisFiyat);
            }

            TaskHistory th = new TaskHistory
            {
                Id = 0,
                TaskId = t.Id,
                Carrier = null,
                CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                CreatedDate = DateTime.Now,
                IsActive = true,
                StatusId = taskDto.Id == 0 ? User.Claims.Where(w => w.Type.Contains("role")).First().Value == Enums.RoleEnum.Kullanici.ToString() ? (int)Enums.StatusEnum.IsIletildi : (int)Enums.StatusEnum.IsOlusturuldu : (int)Enums.StatusEnum.Duzenlendi,
                Description = taskDto.Id == 0 ? "" : Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value) + " - tarafından düzenlendi.",
            };
            _taskHistoryService.TAdd(th);

            _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
            return RedirectToAction("Index");
        }

        public void GetCompanysAndCurrier()
        {
            List<SelectListItem> companysValues = (from x in _companyService.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.Id.ToString()
                                                   }).ToList();

            List<SelectListItem> carrierValues = (from x in _userService.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.NameSurname,
                                                      Value = x.Id.ToString()
                                                  }).ToList();
            List<SelectListItem> cityValues = (from x in _cityService.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();

            ViewBag.Companys = companysValues;
            ViewBag.Carriers = carrierValues;
            ViewBag.Citys = cityValues;
        }

        [HttpPost]
        public IActionResult GetTaskById(int taskId)
        {
            var result = _taskService.GetTaskAndCityByTaskId(taskId);
            if (result.CompanyWillPayId == result.SendCompanyId)
            {
                result.CompanyWillPayId = 1;
            }
            else
            {
                result.CompanyWillPayId = 2;
            }
            return Json(result);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> TaskHistoryDetailsAsync(int taskId)
        {
            await GetKurye();
            var taskHist = _taskService.GetTaskWithCompanyAndCityAndStatusByTaskId(taskId);

            return PartialView("TaskHistoryDetails", taskHist);
        }

        public async System.Threading.Tasks.Task GetKurye()
        {
            var asd = await _userManager.GetUsersInRoleAsync("Kurye");
            List<SelectListItem> carrierValues = (from x in asd
                                                  select new SelectListItem
                                                  {
                                                      Text = x.NameSurname,
                                                      Value = x.Id.ToString()
                                                  }).ToList();
            ViewBag.Carriers = carrierValues;
        }

        [HttpPost]
        public IActionResult TaskOrientations()
        {
            GetCompanysAndCurrier();
            var taskHist = _taskService.GetListWithCompanyAndCityAndStatus2().Where(w => w.Carrier == Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value) && w.StatusId == (int)SigortaApp.Entity.Enums.StatusEnum.TeslimAlindi).ToList();

            return PartialView("TaskOrientations", taskHist);
        }

        [HttpPost]
        public IActionResult TaskOrientationsUpdate(int[] tasks)
        {
            foreach (var item in tasks)
            {
                var result = _taskService.TGetById(item);
                var otogarControl = _cityService.TGetById(result.CityId).Controller;
                if (result != null)
                {
                    result.Carrier = null;
                    result.StatusId = (int)SigortaApp.Entity.Enums.StatusEnum.OtogaraYonlendirildi;
                    _taskService.TUpdate(result);

                    TaskHistory th = new TaskHistory()
                    {
                        Id = 0,
                        Carrier = null,
                        CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        StatusId = (int)SigortaApp.Entity.Enums.StatusEnum.OtogaraYonlendirildi,
                        TaskId = item
                    };

                    _taskHistoryService.TAdd(th);
                    Whatsapp(result, WhatsappEnum.OtogaraYonlendirildi, "");
                }
            }
            //GetCompanysAndCurrier();
            //var taskHist = _taskService.GetListWithCompanyAndCityAndStatus2().Where(w => w.Carrier == Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value) && w.StatusId == (int)SigortaApp.Entity.Enums.StatusEnum.TeslimAlindi).ToList();
            _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
            return RedirectToAction("Index");
        }

        public IActionResult PutOnHold(Task task)
        {
            var value = _taskService.TGetById(task.Id);
            if (value.StatusId != (int)Enums.StatusEnum.BeklemeyeAlindi)
            {
                value.StatusId = (int)Enums.StatusEnum.BeklemeyeAlindi;
                _taskService.TUpdate(value);

                TaskHistory th = new TaskHistory
                {
                    TaskId = task.Id,
                    StatusId = (int)Enums.StatusEnum.BeklemeyeAlindi,
                    Carrier = value.Carrier ?? null,
                    CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    PutOnHoldDate = task.CreatedDate,
                    Description = task.Description
                };

                _taskHistoryService.TAdd(th);
                //var taskHist = _taskService.GetListWithCompanyAndCityAndStatus2().Where(w => w.Id == task.Id).FirstOrDefault();

                //return PartialView("TaskHistoryDetails", taskHist);
                _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
                return RedirectToAction("Index");
            }
            else
            {
                //var taskHist = _taskService.GetListWithCompanyAndCityAndStatus2().Where(w => w.Id == task.Id).FirstOrDefault();
                //return PartialView("TaskHistoryDetails", taskHist);
                _notyfService.Error("İşem Başarısız Oldu.");
                return RedirectToAction("Index");
            }
        }

        public IActionResult CarrierRedirectWork(Task task)
        {
            var value = _taskService.TGetById(task.Id);
            var user = new AppUser();
            if (value.Carrier != null)
            {
                user = _userService.TGetById(value.Carrier.Value);
            }

            if (value.StatusId != (int)Enums.StatusEnum.KuryeGeriYonlendirdi)
            {
                value.StatusId = (int)Enums.StatusEnum.KuryeGeriYonlendirdi;
                value.Carrier = null;
                _taskService.TUpdate(value);

                TaskHistory th = new TaskHistory
                {
                    TaskId = task.Id,
                    StatusId = (int)Enums.StatusEnum.KuryeGeriYonlendirdi,
                    Carrier = null,
                    CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Description = task.Description + " (" + user.NameSurname + ")"
                };

                _taskHistoryService.TAdd(th);
                _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
                return RedirectToAction("Index");
            }
            else
            {
                _notyfService.Error("İşem Başarısız Oldu.");
                return RedirectToAction("Index");
            }
        }

        public IActionResult EndTask()
        {

            var tasks = new List<SelectListItem>();
            foreach (var item in _taskService.GetListWithCompanyCityAndStatus().Where(w => w.StatusId == (int)Enums.StatusEnum.OtogaraYonlendirildi))
            {
                tasks.Add(new SelectListItem
                {
                    Text = "İş No:" + item.Id.ToString() + " - İl:" + item.CityUst.Name + " - Şirket:" + item.OrderCompany.Name,
                    Value = item.Id.ToString()
                });
            }
            ViewBag.Tasks = tasks;
            return View();
        }

        public IActionResult EndTaskFunc(EndTaskDto endTask)
        {
            if (endTask.Task.Count() > 0)
            {
                foreach (var item in endTask.Task)
                {
                    var endingTask = _taskService.TGetById(Convert.ToInt32(item));
                    endingTask.StatusId = (int)Enums.StatusEnum.IsBitirildi;
                    _taskService.TUpdate(endingTask);

                    TaskHistory th = new TaskHistory
                    {
                        TaskId = endingTask.Id,
                        StatusId = (int)Enums.StatusEnum.IsBitirildi,
                        Carrier = endingTask.Carrier.Value,
                        CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        PutOnHoldDate = null,
                        Description = "",
                        End_BusDate = Convert.ToDateTime(endTask.BusDate),
                        End_BusPhone = endTask.OtoTel,
                        End_BusPrice = Convert.ToDouble(endTask.OtobüsUcret)
                    };
                    _taskHistoryService.TAdd(th);
                }

                //Files Database yazılacak. Ona göre files işlemleri yapılarak kayıt edilecek. TaskId üzerinden gidilecek.
                List<int> filesId = new List<int>();

                if (endTask.Files.Count > 0)
                {
                    foreach (var item in endTask.Files)
                    {
                        var fileName = Path.GetFileName(item.FileName);
                        var fileExtension = Path.GetExtension(fileName);
                        var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                        var objfiles = new Files()
                        {
                            Id = 0,
                            Name = newFileName,
                            FileType = fileExtension,
                            CreatedOn = DateTime.Now,
                            CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                            IsActive = true
                        };

                        using (var target = new MemoryStream())
                        {
                            item.CopyTo(target);
                            objfiles.DataFiles = target.ToArray();
                        }

                        _filesService.TAdd(objfiles);

                        filesId.Add(objfiles.Id);
                    }
                }

                if (endTask.FilesParca.Count > 0)
                {
                    foreach (var item in endTask.FilesParca)
                    {
                        var fileName = Path.GetFileName(item.FileName);
                        var fileExtension = Path.GetExtension(fileName);
                        var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                        var objfiles = new Files()
                        {
                            Id = 0,
                            Name = newFileName,
                            FileType = fileExtension,
                            CreatedOn = DateTime.Now,
                            CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                            IsActive = true
                        };

                        using (var target = new MemoryStream())
                        {
                            item.CopyTo(target);
                            objfiles.DataFiles = target.ToArray();
                        }

                        _filesService.TAdd(objfiles);

                        filesId.Add(objfiles.Id);
                    }
                }

                if (filesId.Count > 0)
                {
                    foreach (var item in filesId)
                    {
                        foreach (var item2 in endTask.Task)
                        {
                            FileTask fileTask = new FileTask()
                            {
                                Id = 0,
                                FilesId = item,
                                TaskId = Convert.ToInt32(item2),
                                IsActive = true,
                                CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                                CreatedDate = DateTime.Now
                            };
                            _fileTaskService.TAdd(fileTask);
                        }
                    }
                }

                var personCompany = _companyService.GetList().Where(w => w.PersonId == Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).FirstOrDefault();
                Payment pay1 = new Payment
                {
                    PaymentMethodId = (int)Enums.OdemeTuruEnum.Nakit,
                    CompanyId = personCompany != null ? personCompany.Id : Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                    CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                    CreatedDate = DateTime.Now,
                    Price = Convert.ToDecimal(endTask.OtobüsUcret),
                    StatusId = (int)Enums.OdemeTuruEnum.Borclandirma,
                    Description = endTask.Task.First().ToString() + " Nolu iş numarasından (Otobüs Ücreti)",
                    TaskId = Convert.ToInt32(endTask.Task.First()),
                };
                _paymentService.TAdd(pay1);

                foreach (var item in endTask.Task)
                {
                    var endingTask = _taskService.TGetById(Convert.ToInt32(item));
                    Whatsapp(endingTask, WhatsappEnum.IsBitirildi, "");
                }
            }


            return RedirectToAction("Index");
        }

        public IActionResult CarrierAssign(int taskId, int personId)
        {
            var value = _taskService.TGetById(taskId);
            if (value.StatusId != (int)Enums.StatusEnum.EkibeAtandi)
            {
                value.StatusId = (int)Enums.StatusEnum.EkibeAtandi;
                value.Carrier = personId;
                _taskService.TUpdate(value);

                TaskHistory th = new TaskHistory
                {
                    TaskId = taskId,
                    StatusId = (int)Enums.StatusEnum.EkibeAtandi,
                    Carrier = value.Carrier ?? null,
                    CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                _taskHistoryService.TAdd(th);
                //var taskHist = _taskService.GetListWithCompanyAndCityAndStatus2().Where(w => w.Id == taskId).FirstOrDefault();

                //return PartialView("TaskHistoryDetails", taskHist);
                _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
                return RedirectToAction("Index");
            }
            else
            {
                //var taskHist = _taskService.GetListWithCompanyAndCityAndStatus2().Where(w => w.Id == taskId).FirstOrDefault();
                //return PartialView("TaskHistoryDetails", taskHist);
                _notyfService.Success("İşem Başarısız Oldu.");
                return RedirectToAction("Index");
            }
        }

        public IActionResult Received(TaskDto task)
        {
            var value = _taskService.TGetById(task.Id);
            if (value.StatusId != (int)Enums.StatusEnum.TeslimAlindi)
            {
                value.StatusId = (int)Enums.StatusEnum.TeslimAlindi;
                _taskService.TUpdate(value);

                TaskHistory th = new TaskHistory
                {
                    TaskId = task.Id,
                    StatusId = (int)Enums.StatusEnum.TeslimAlindi,
                    Carrier = value.Carrier.Value,
                    CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                _taskHistoryService.TAdd(th);

                //PaymentTask pt = new PaymentTask
                //{
                //    TaskId = task.Id,
                //    CariMi = task.CariMi != true ? false : true,
                //    Price = task.Price,
                //    CompanyId = value.CompanyWillPayId,
                //    PaymentReceived = false,
                //    CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                //    CreatedDate = DateTime.Now
                //};

                //_paymentTaskService.TAdd(pt);

                //Her durumda borçlandırma şirkete yapılacak.
                Payment pay = new Payment
                {
                    PaymentMethodId = (int)Enums.OdemeTuruEnum.AcikHesap,
                    CompanyId = value.CompanyWillPayId,
                    CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                    CreatedDate = DateTime.Now,
                    Price = Convert.ToDecimal(task.Price),
                    StatusId = (int)Enums.OdemeTuruEnum.Borclandirma,
                    Description = task.Id + " Nolu iş numarasından borçlandırma",
                    TaskId = task.Id,
                };
                _paymentService.TAdd(pay);

                //Borçlandırma Yani VADE Seçildiyse!!!

                if (!task.CariMi)
                {
                    var personCompany = _companyService.GetList().Where(w => w.PersonId == Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value)).FirstOrDefault();
                    Payment pay1 = new Payment
                    {
                        PaymentMethodId = (int)Enums.OdemeTuruEnum.Nakit,
                        CompanyId = personCompany != null ? personCompany.Id : Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                        CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                        CreatedDate = DateTime.Now,
                        Price = Convert.ToDecimal(task.Price),
                        StatusId = (int)Enums.OdemeTuruEnum.Borclandirma,
                        Description = task.Id + " Nolu iş numarasından",
                        TaskId = task.Id,
                    };
                    _paymentService.TAdd(pay1);

                    Payment pay2 = new Payment
                    {
                        PaymentMethodId = (int)Enums.OdemeTuruEnum.Nakit,
                        CompanyId = value.CompanyWillPayId,
                        CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                        CreatedDate = DateTime.Now,
                        Price = Convert.ToDecimal(task.Price),
                        Description = task.Id + " Nolu iş numarasından",
                        StatusId = (int)Enums.OdemeTuruEnum.Tahsilat,
                        TaskId = task.Id,
                    };
                    _paymentService.TAdd(pay2);
                }


                //var taskHist = _taskService.GetListWithCompanyAndCityAndStatus2().Where(w => w.Id == task.Id).FirstOrDefault();

                //return PartialView("TaskHistoryDetails", taskHist);
                _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
                return RedirectToAction("Index");
            }
            else
            {
                //var taskHist = _taskService.GetListWithCompanyAndCityAndStatus2().Where(w => w.Id == task.Id).FirstOrDefault();
                //return PartialView("TaskHistoryDetails", taskHist);
                _notyfService.Success("İşem Başarısız Oldu.");
                return RedirectToAction("Index");
            }
        }

        public IActionResult CityUstByCity(int cityId)
        {
            List<SelectListItem> cityValues = (from x in _cityService.GetList()
                                               where x.UstId == cityId
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            return Json(cityValues);
        }

        public IActionResult TakeTheTask(int Id)
        {
            var task = _taskService.TGetById(Id);
            if (task != null)
            {
                task.Carrier = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value);
                task.StatusId = (int)Enums.StatusEnum.EkibeAtandi;
                _taskService.TUpdate(task);

                TaskHistory th = new TaskHistory
                {
                    TaskId = task.Id,
                    StatusId = (int)Enums.StatusEnum.EkibeAtandi,
                    Carrier = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                    CreatedBy = Convert.ToInt32(User.Claims.Where(w => w.Type.Contains("nameidentifier")).First().Value),
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };
                _taskHistoryService.TAdd(th);
                _notyfService.Success("İşem Başarılı bir şekilde tamamlandı.");
                return RedirectToAction("Index");
            }
            _notyfService.Success("İşem Başarısız Oldu.");
            return RedirectToAction("Index");
        }
    }
}