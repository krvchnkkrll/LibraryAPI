using LibraryAPI.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Infrastructure.LoggingEntities;

namespace LibraryAPI.Application.Handlers.Authors.Commands.Update;

public record UpdateAuthor(int Id, AuthorCommandDto Dto) : IRequest;

file sealed class UpdateAuthorHandler : IRequestHandler<UpdateAuthor>
{
    private readonly LibraryInfoContext _context;
    private readonly EntityChangeLogging _logger;

    public UpdateAuthorHandler(LibraryInfoContext context, EntityChangeLogging logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(UpdateAuthor request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(
            a => a.Id == request.Id, cancellationToken);
        
        if (author == null)
        {
            throw new KeyNotFoundException($"Автор с индексом {request.Id} не найден.");
        }
        
        author.Surname = request.Dto.Surname;
        author.Name = request.Dto.Name;
        author.Patronymic = request.Dto.Patronymic;

        _context.Authors.Update(author);
        
        var entries = _context.ChangeTracker.Entries();
        _logger.LogChanges(entries);
        
        await _context.SaveChangesAsync(cancellationToken);
        
    }
}