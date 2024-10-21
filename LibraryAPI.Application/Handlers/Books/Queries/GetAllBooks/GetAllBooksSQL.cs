using Dapper;
using LibraryAPI.Infrastructure.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace LibraryAPI.Application.Handlers.Books.Queries.GetAllBooks;

public sealed record GetAllBooksSql : IRequest<List<BooksQueriesSqlDto>>;

file sealed class GetAllBooksHandler : IRequestHandler<GetAllBooksSql, List<BooksQueriesSqlDto>>
{
    private readonly string _connectionString;

    public GetAllBooksHandler(LibraryInfoContext context)
    {
        _connectionString = context.Database.GetConnectionString() ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<List<BooksQueriesSqlDto>> Handle(GetAllBooksSql request, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        
        await connection.OpenAsync(cancellationToken);
        
        var sqlQuery = $"SELECT \n    b.\"Id\" AS \"Id\",\n    b.\"Name\" AS \"Name\",\n    b.\"Genre\" AS \"Genre\",\n    b.\"AuthorId\" AS \"AuthorId\",\n    b.\"BookStatus\" AS \"BookStatus\",\n    a.\"Name\" AS \"AuthorName\",\n    a.\"Surname\" AS \"AuthorSurname\",\n    a.\"Patronymic\" AS \"AuthorPatronymic\"\nFROM \n    \"Books\" b \nLEFT JOIN \n    \"Authors\" a ON b.\"AuthorId\" = a.\"Id\";";

        var books = await connection.QueryAsync<BooksQueriesSqlDto>(sqlQuery);
        
        var booksToReturn = books.Select(b => new BooksQueriesSqlDto()
        {
            Id = b.Id,
            Name = b.Name,
            Genre = b.Genre,
            AuthorId = b.AuthorId,
            AuthorSurname = b.AuthorSurname,
            AuthorName = b.AuthorName,
            AuthorPatronymic = b.AuthorPatronymic, 
            BookStatus = b.BookStatus
        }).ToList();
        
        return booksToReturn;
    }
}