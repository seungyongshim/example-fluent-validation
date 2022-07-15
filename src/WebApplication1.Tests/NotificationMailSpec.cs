using FluentValidation;
using WebApplication1.Dto;

namespace WebApplication1.Tests;

public class NotificationMailSpec
{
    static NotificationMailSpec()
    {
        
    }

    [Fact]
    public void Valid()
    {
        var sut = new NotificationMail("Hello", null, null, null);

        var ret = new NotificationMailValidator().Validate(sut);
    }
}
