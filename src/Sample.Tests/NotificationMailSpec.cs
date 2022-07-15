using FluentValidation;
using Sample.Dto;
using Xunit;

namespace Sample.Tests;

public class NotificationMailSpec
{
    static NotificationMailSpec()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }

    [Fact]
    public void Valid()
    {
        var sut = new NotificationMail("Hello", null, null, null);

        var ret = new NotificationMailValidator().Validate(sut);
    }
}
