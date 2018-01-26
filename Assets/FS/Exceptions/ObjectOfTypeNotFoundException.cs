using System;

namespace FS.Exceptions
{
    public class ObjectOfTypeNotFoundException : Exception
    {
        public ObjectOfTypeNotFoundException(Type objectType, string message = ""):
            base(objectType.FullName + (string.IsNullOrEmpty(message) ? "" : (" | " + message))) { }
    }
}
