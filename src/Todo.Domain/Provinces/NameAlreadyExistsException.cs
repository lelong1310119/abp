using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Todo.Provinces
{
    public class NameAlreadyExistsException : BusinessException
    {
        public NameAlreadyExistsException(string name)
            : base(TodoDomainErrorCodes.NameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
