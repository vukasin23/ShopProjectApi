using Azure.Core;
using ShopProject.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopProject.Implementation
{
    public class UseCaseHandler
    {
        private readonly IApplicationActor _actor;
        //private readonly IUseCaseLogger _logger;

        public UseCaseHandler(IApplicationActor actor)
        {
            _actor = actor;
            //_logger = logger;
        }

        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            HandleActorUseCase(command);
            //_logger.Log(_actor, command, request);
            command.Execute(request);
        }

        public TResult HandleQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
        {
            HandleActorUseCase(query);
            //_logger.Log(_actor, query, search);
            var result = query.Execute(search);
            return result;
        }
        private void HandleActorUseCase(IUseCase useCase)
        {
            if (!_actor.AllowedUseCases.Contains(useCase.Id))
            {
                throw new UnauthorizedAccessException("You are not allowed to execute this command.");
            }
        }
    }
}
