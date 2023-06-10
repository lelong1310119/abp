using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Todo.Communes
{
    public class CommuneCodeAlreadyExistsException : BusinessException
    {
        public CommuneCodeAlreadyExistsException(string code)
            : base(TodoDomainErrorCodes.CommuneCodeAlreadyExists)
        {
            WithData("code", code);
        }
    }
}
