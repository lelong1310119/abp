using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Todo.Provinces
{
    public class AreaCodeAlreadyExistsException : BusinessException
    {
        public AreaCodeAlreadyExistsException(string areaCode)
            : base(TodoDomainErrorCodes.AreaCodeAlreadyExists)
        {
            WithData("areaCode", areaCode);
        }
    }
}
