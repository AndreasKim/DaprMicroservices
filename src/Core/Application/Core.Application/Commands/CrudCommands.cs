using AutoMapper;
using Core.Application.Interfaces;
using MediatR;

namespace Core.Application.Commands
{

    public class CreateCommandHandler<TModel, TCommand> : RepositoryCommand<TModel, TCommand>
        where TModel : class
        where TCommand : IRequest<TModel>
    {
        public CreateCommandHandler(IRepository<TModel> repository, IMapper mapper)
            : base(repository, mapper, async p => await repository.AddAsync(p)) { }
    }

    public class UpdateCommandHandler<TModel, TCommand> : RepositoryCommand<TModel, TCommand>
        where TModel : class
        where TCommand : IRequest<TModel>
    {
        public UpdateCommandHandler(IRepository<TModel> repository, IMapper mapper)
            : base(repository, mapper, async p => await repository.UpdateAsync(p)) { }
    }

    public class DeleteCommandHandler<TModel, TCommand> : RepositoryCommand<TModel, TCommand>
        where TModel : class
        where TCommand : IRequest<TModel>
    {
        public DeleteCommandHandler(IRepository<TModel> repository, IMapper mapper)
            : base(repository, mapper, async p => await repository.DeleteAsync(p)) { }
    }
}
