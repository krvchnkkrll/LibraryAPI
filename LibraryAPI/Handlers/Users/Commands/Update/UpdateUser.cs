using LibraryAPI.DbContext;
using LibraryAPI.Handlers.Authors.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Handlers.Users.Commands.Update;

public record UpdateUser(int Id, UserCommandDto Dto) : IRequest;

file sealed class UpdateUserHadler : IRequest<UpdateUser>
{
    private readonly LibraryInfoContext _context;

    public UpdateUserHadler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            u => u.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"Пользователь с индексом {request.Id} не найден.");
        }

        user.Surname = request.Dto.Surname;
        user.Name = request.Dto.Name;
        user.Patronymic = request.Dto.Patronymic;
        user.Login = request.Dto.Login;
        user.Password = request.Dto.Password;
        user.IsItStaff = request.Dto.IsItStaff;

        _context.Users.Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
    
    
}