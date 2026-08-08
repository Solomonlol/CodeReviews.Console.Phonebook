using Backend.Models.Dto;
using Backend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Backend.Interfaces
{
    internal interface IService<T, TDto> where T : class where TDto : class
    {
        Task<TDto?> GetUserById(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TDto>> GetList(CancellationToken cancellationToken = default);
        Task CreateAsync(TDto item, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, TDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
