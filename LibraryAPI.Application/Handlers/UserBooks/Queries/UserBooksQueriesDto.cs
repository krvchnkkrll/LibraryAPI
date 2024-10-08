﻿using LibraryAPI.Application.Handlers.Books.Queries;
using LibraryAPI.Application.Handlers.Users.Queries;

namespace LibraryAPI.Application.Handlers.UserBooks.Queries;

public class UserBooksQueriesDto
{
    
    /// <summary>
    /// Идентификатор записи выдачи книги пользователю
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор пользователя получившего книгу
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Пользователь получивший книгу
    /// </summary>
    public UserQueriesDto? User { get; set; }
    
    /// <summary>
    /// Идентификатор книги
    /// </summary>
    public int BookId { get; set; }
    
    /// <summary>
    /// Книга, которую забрал пользователь
    /// </summary>
    public BooksQueriesDto? Book { get; set; }
    
    /// <summary>
    /// Дата получения книги
    /// </summary>
    public DateTime DateReceipt { get; set; }
    
    /// <summary>
    /// Срок выдачи
    /// </summary>
    public int BorrowPeriod { get; set; }
    
    /// <summary>
    /// Дата возврата книги
    /// </summary>
    public DateTime? DateReturn { get; set; }
}