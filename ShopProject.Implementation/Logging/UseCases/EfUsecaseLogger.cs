using ShopProject.Application;
using System;
using System.Collections.Generic;
using System.Text;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Logging.UseCases
{
    public class EfUsecaseLogger : IUseCaseLogger
    {
        private readonly ShopProjectContext  _context;

        public EfUsecaseLogger(ShopProjectContext context)
        {
            _context = context;
        }
        public void Log(IApplicationActor actor, IUseCase useCase)
        {
            var log = new Domain.UseCaseLog
            {
                ActorId = actor.Id,
                UseCaseId = useCase.Id,
                UseCaseName = useCase.Name
            };
            
            _context.UseCaseLogs.Add(log);
            _context.SaveChanges();
        }
    }
}
