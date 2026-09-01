using Microsoft.Extensions.Logging;
using ShopProject.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Logging.UseCases
{
    public class ConsoleUsecaseLogger : IUseCaseLogger
    {
        public void Log(IApplicationActor actor, IUseCase useCase, object data)
        {
            throw new NotImplementedException();
        }
    }
}
