using System.Collections.Generic;

namespace Slay.Utilities.ServiceResult
{
    public abstract class ServiceResultBase
    {
        public bool HasErrors { get; set; }

        public IEnumerable<Error> Errors { get; set; }
    }
}