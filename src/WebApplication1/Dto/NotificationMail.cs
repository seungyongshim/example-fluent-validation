
using FluentValidation;

namespace WebApplication1.Dto;

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

