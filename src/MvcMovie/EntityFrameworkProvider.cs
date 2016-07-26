using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MvcMovie.Data;

namespace MvcMovie.CustomeProvider
{
    internal class EntityFrameworkProvider : ConfigurationProvider
    {
        private Action<DbContextOptionsBuilder> _optionAction;

        public EntityFrameworkProvider(Action<DbContextOptionsBuilder> _optionAction)
        {
            this._optionAction = _optionAction;
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            _optionAction(builder);
            using (var dbContext = new ApplicationDbContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();
                Data = !dbContext.Values.Any()
                    ? CreateAndSaveDefaultValue(dbContext)
                    : dbContext.Values.ToDictionary(x => x.Id, x => x.Value);

            }
        }

        private IDictionary<string, string> CreateAndSaveDefaultValue(ApplicationDbContext dbContext)
        {
            var configValues = new Dictionary<string,string>()
            {
                {"name","viet anh" },
                {"address","ho chi minh" }
            };
            dbContext.Values.AddRange(configValues.Select(x=>new ConfigurationValue()
            {
                Id=x.Key,
                Value = x.Value
            }).ToArray());
            dbContext.SaveChanges();
            return configValues;
        }
    }
}