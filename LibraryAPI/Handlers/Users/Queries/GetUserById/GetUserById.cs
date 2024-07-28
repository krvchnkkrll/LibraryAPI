using LibraryAPI.DbContext;
using LibraryAPI.Handlers.Authors.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Handlers.Users.Queries.GetUserById;

public sealed record GetUserById(int Id) : IRequest<UserQueriesDto>;

file sealed class GetUserByIdHandler : IRequestHandler<GetUserById, UserQueriesDto>
{
    private readonly LibraryInfoContext _context;

    public GetUserByIdHandler(LibraryInfoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UserQueriesDto> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            u => u.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"Пользователя с индексом {request.Id} не найден");
        }

        var userToReturn = new UserQueriesDto()
        {
            Id = user.Id,
            Surname = user.Surname,
            Name = user.Name,
            Patronymic = user.Patronymic,
            IsItStaff = user.IsItStaff,
            Login = user.Login,
            Password = user.Password
        };

        return userToReturn;
    }
}