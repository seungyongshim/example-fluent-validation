namespace WebApplication3.Dto;

public record NotificationMailDto
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

