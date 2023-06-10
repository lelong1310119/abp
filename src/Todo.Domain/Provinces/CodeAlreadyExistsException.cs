using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Todo.Provinces
{
    public class CodeAlreadyExistsException : BusinessException
    {
        public CodeAlreadyExistsException(string code)
            : base(TodoDomainErrorCodes.CodeAlreadyExists)
        {
            WithData("code", code);
        }
    }
}
