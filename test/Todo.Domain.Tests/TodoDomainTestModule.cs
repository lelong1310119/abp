using Todo.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Todo;

[DependsOn(
    typeof(TodoEntityFrameworkCoreTestModule)
    )]
public class TodoDomainTestModule : AbpModule
{

}
