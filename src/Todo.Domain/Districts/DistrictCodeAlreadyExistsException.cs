using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Todo.Districts
{
    public class DistrictCodeAlreadyExistsException : BusinessException
    {
        public DistrictCodeAlreadyExistsException(string code)
            : base(TodoDomainErrorCodes.DistrictCodeAlreadyExists)
        {
            WithData("code", code);
        }
    }
}
