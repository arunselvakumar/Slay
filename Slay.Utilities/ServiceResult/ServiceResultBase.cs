using System.Collections.Generic;
using System.Linq;
using Slay.Utilities.Extensions;

namespace Slay.Utilities.ServiceResult
{
    public abstract class ServiceResultBase
    {
	    public IEnumerable<Error> Errors { get; set; }

	    public bool HasErrors => this.Errors.IsAny();
    }
}