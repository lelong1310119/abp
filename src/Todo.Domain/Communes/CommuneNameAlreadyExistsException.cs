using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Todo.Communes
{
    public class CommuneNameAlreadyExistsException : BusinessException
    {
        public CommuneNameAlreadyExistsException(string name)
            : base(TodoDomainErrorCodes.CommuneNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
