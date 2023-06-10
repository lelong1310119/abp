using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Todo.Districts
{
    public class DistrictNameAlreadyExistsException : BusinessException
    {
        public DistrictNameAlreadyExistsException(string name)
            : base(TodoDomainErrorCodes.DistrictNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
