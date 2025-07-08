// <copyright file="UnitOfWork.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Persistence.UoW
{
    using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
    using LibraryManagementCleanArchitecture.Infastrucute.Persistence.Context;
    using Microsoft.EntityFrameworkCore.Storage;

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
                this.currentTransaction = await this.context.Database.BeginTransactionAsync();
                var result = await this.context.SaveChangesAsync();
                await this.currentTransaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                if (this.currentTransaction != null)
                {
                    await this.currentTransaction.RollbackAsync();
                }

                throw;
            }
            finally
            {
                if (this.currentTransaction != null)
                {
                    await this.currentTransaction.DisposeAsync();
                    this.currentTransaction = null;
                }
            }
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
