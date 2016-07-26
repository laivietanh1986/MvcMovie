using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace MvcMovie.CustomeProvider
{
    public class EntityFrameworkConfigurationSource:IConfigurationSource
    {
        private readonly Action<DbContextOptionsBuilder> _optionAction;

        public EntityFrameworkConfigurationSource(Action<DbContextOptionsBuilder> optionAction)
        {
            _optionAction = optionAction;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EntityFrameworkProvider(_optionAction);
        }
    }
}
