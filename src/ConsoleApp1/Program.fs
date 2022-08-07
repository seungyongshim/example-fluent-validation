let createEmailNotification templates msg (user : UserEmailData) =
    let { SubjectLine = subjectTemplate; Content = contentTemplate } =
        templates
        |> Map.tryFind user.Localization
        |> Option.defaultValue (Map.find Localizations.english templates)
 
    let r =
        Templating.append
            (Templating.replacementOfEnvelope msg)
            (Templating.replacementOfFlatRecord user)
    let subject = Templating.run subjectTemplate r
    let content = Templating.run contentTemplate r
    
    {
        RecipientUserId = user.UserId
        EmailAddress = user.EmailAddress
        NotificationSubjectLine = subject
        NotificationText = content
        CreatedDate = DateTime.UtcNow
    }
