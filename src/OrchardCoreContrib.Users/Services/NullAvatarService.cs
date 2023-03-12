using System;

namespace OrchardCoreContrib.Users.Services;

public class NullAvatarService : IAvatarService
{
    public string Generate(string userName) => String.Empty;
}
