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
        private readonly IUseCaseLogger _logger;

        public UseCaseHandler(IApplicationActor actor,  IUseCaseLogger logger)
        {
            _actor = actor;
            _logger = logger;
        }

        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            //HandleActorUseCase(command);
            _logger.Log(_actor, command);
            command.Execute(request);
        }

        public PagedResponse<TData> HandleQuery<TSearch, TData>(IQuery<TSearch, TData> query, TSearch search)
            where TSearch : PagedSearch
        {
            //HandleActorUseCase(query);
            _logger.Log(_actor, query);
            return query.Execute(search);
        }

        public TData HandleGetOne<TData, TSearch>(IGetByOne<TData, TSearch> query, TSearch search)
        {
            _logger.Log(_actor, query);
            return query.Execute(search);
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
