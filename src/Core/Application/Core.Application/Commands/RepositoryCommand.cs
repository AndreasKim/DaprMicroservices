using AutoMapper;
using Core.Application.Common.Interfaces;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Commands
{
    public class RepositoryCommand<TModel, TCommand> : IRequestHandler<TCommand, TModel>
        where TModel : class
        where TCommand : IRequest<TModel>
    {
        private readonly Func<TModel, Task> repositoryAction;

        public RepositoryCommand(IRepository<TModel> repository, IMapper mapper, Func<TModel, Task> repositoryAction)
        {
            Repository = repository;
            Mapper = mapper;
            this.repositoryAction = repositoryAction ?? throw new ArgumentNullException(nameof(repositoryAction));
        }

        protected IRepository<TModel> Repository { get; }
        protected IMapper Mapper { get; }

        public virtual async Task<TModel> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var product = Mapper.Map<TModel>(request);

            await repositoryAction.Invoke(product);
            return product;
        }
    }
}
