using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class NewsLetterService : INewsLetterService
    {
        INewsLetterRepository _newsletterDal;

        public NewsLetterService(INewsLetterRepository newsletterDal)
        {
            _newsletterDal = newsletterDal;
        }

        public void AddNewsLetter(NewsLetter newsletter)
        {
            _newsletterDal.Insert(newsletter);
        }
    }
}
