using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Core.Application;
using LibraryManagementCleanArchitecture.Domain.Entities;
using LibraryManagementCleanArchitecture.Infastrucute.Persistence.Context;
using LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryManagementCleanArchitecture.Persistence.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private IDbContextTransaction? currentTransaction;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                currentTransaction = await context.Database.BeginTransactionAsync();
                var result = await context.SaveChangesAsync();
                await currentTransaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                if (currentTransaction != null)
                {
                    await currentTransaction.RollbackAsync();
                }
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    await currentTransaction.DisposeAsync();
                    currentTransaction = null;
                }
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
