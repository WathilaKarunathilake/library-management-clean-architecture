using LibraryManagementCleanArchitecture.Core.Application;
using LibraryManagementCleanArchitecture.Domain.Entities;

namespace LibraryManagementCleanArchitecture.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
