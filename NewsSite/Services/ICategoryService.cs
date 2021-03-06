﻿namespace NewsSite.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NewsSite.Models.Data;
    using NewsSite.Models.Input;

    public interface ICategoryService
    {
        Task AddAsync(string name, string description);

        Task<Category> GetByIdAsync(int id, bool returnArticles);

        Task<Category> GetByNameAsync(string name, bool returnArticles);

        Task<IEnumerable<Category>> GetAllAsync();
    }
}
