
using FluentValidation;

namespace Sample.Dto;

public record NotificationMail
(
    string Email,
    string Name,
    string Body,
    Attachment Attachment
);


public record Attachment
(
    string Filename,
    byte[] Bytes
);

