using AutoMapper;
using Core.Application.Common.Interfaces;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Commands
{
    public abstract class BaseCommand<TModel, TCommand> : IRequestHandler<TCommand, TModel>
        where TModel : class
        where TCommand : IRequest<TModel>
    {
        public BaseCommand(IRepository<TModel> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        protected IRepository<TModel> Repository { get; }
        protected IMapper Mapper { get; }

        public abstract Task<TModel> Handle(TCommand request, CancellationToken cancellationToken);

    }
}
