using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application
{
    public interface IUseCaseLogger
    {
        void Log(IApplicationActor actor, IUseCase useCase, object data);
    }
}
