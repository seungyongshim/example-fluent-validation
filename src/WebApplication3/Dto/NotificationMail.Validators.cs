using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApplication3.Dto;

public class NotificationMailValidator : AbstractValidator<NotificationMailDto>
{
    public NotificationMailValidator()
    {
        RuleFor(x => x.Email).NotNull().EmailAddress();
        RuleFor(x => x.Attachment).SetValidator(new AttachmentValidator());
    }
}

public class AttachmentValidator : AbstractValidator<Attachment>
{
    public AttachmentValidator()
    {
        RuleFor(x => x.Filename).NotEmpty().NotNull();
        RuleFor(x => x.Bytes).NotNull();
    }
}
