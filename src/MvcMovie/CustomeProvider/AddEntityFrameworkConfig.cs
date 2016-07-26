using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MvcMovie.CustomeProvider
{
    public static class AddEntityFrameworkExtensions
    {
        public static IConfigurationBuilder AddEntityFrameworkConfig(this IConfigurationBuilder builder,
            Action<DbContextOptionsBuilder> setup)
        {
            return builder.Add(new EntityFrameworkConfigurationSource(setup));
        }
    }
}
