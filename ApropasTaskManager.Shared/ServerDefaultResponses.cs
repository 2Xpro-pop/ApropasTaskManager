using System;
using System.Collections.Generic;
using System.Text;

namespace ApropasTaskManager.Shared
{
    public static class ServerDefaultResponses
    {
        public const string ProjectNotFound = "PROJECT_NOT_FOUND";
        public const string UserNotFound = "USER_NOT_FOUND";
        public const string UserExist = "USER_EXIST";
        public const string MustBeOneDirector = "THERE_MUST_BE_ONLY_ONE_DIRECTOR";
        public const string NotAuthorized = "NOT_AUTHORIZED";
        public const string NetExceptions = "Failed to establish a connection with the server";
    }
}
