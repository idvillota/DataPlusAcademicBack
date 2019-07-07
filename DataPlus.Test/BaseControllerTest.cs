using DataPlus.Contracts.Logger;
using DataPlus.Contracts.Repositories;
using DataPlus.Entities;
using DataPlus.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPlus.Test
{
    public class BaseControllerTest
    {
        public static string _connectionString;
        public ILoggerManager _logger;
        public IWrapperRepository _repositoryWrapper;
        public static DbContextOptions<RepositoryContext> _dbContextOptions { get; set; }

        public BaseControllerTest()
        {
            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            _connectionString = config.GetSection("ConnectionStrings:ConnectionString").Value;

            _dbContextOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(_connectionString)
                .Options;

            var context = new RepositoryContext(_dbContextOptions);
            //Verify if database already contains data;
            _logger = new Mock<ILoggerManager>().Object;
            _repositoryWrapper = new WrapperRepository(context);
            
        }
    }
}
