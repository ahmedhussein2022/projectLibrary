﻿using Library.Models;

namespace Library.Repositories
{
    public interface ISearchRepository
    {
        List<Book>? FindBookByNameOrByISBN(string searchText);
        List<Book>? SearchByCategory(int id);
        List<Book>? SearchByAuthor(int id);
    }
}
