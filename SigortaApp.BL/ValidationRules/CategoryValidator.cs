using SigortaApp.Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori Adını Boş Geçemezsiniz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Kategori Açıklamasını Boş Geçemezsiniz.");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Kategori Adı en fazla 50 karakter olmalıdır.");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Kategori Adı en az 2 karakter olmalıdır.");
        }
    }
}
