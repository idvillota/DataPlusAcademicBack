using DataPlus.Contracts.Logger;
using DataPlus.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPlus.Services
{
    public class BaseService
    {
        internal ILoggerManager _logger;
        internal IWrapperRepository _wrapperRepository;
        
        public BaseService(IWrapperRepository wrapperRepository, ILoggerManager logger)
        {
            _wrapperRepository = wrapperRepository;
            _logger = logger;
        }
    }
}
