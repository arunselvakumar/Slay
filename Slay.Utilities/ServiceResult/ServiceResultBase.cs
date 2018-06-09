namespace Slay.Utilities.ServiceResult
{
    using System.Collections.Generic;

    using Slay.Utilities.Extensions;

    public abstract class ServiceResultBase
    {
        public IEnumerable<Error> Errors { get; set; }

        public bool HasErrors => !this.Errors.IsNullOrEmpty();
    }
}