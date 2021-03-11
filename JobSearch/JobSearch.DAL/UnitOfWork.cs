using JobSearch.DAL.Implementations;
using JobSearch.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;

namespace JobSearch.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext context;

        private IJobCategoryRepository jobCategoryRepository;
        private IJobRepository jobRepository;

        public UnitOfWork(ApplicationContext context)
        {
            this.context = context;
        }

        public IJobCategoryRepository JobCategoryRepository
        {
            get
            {
                return jobCategoryRepository ?? (jobCategoryRepository = new JobCategoryRepository(context));
            }
        }

        public IJobRepository JobRepository
        {
            get
            {
                return jobRepository ?? (jobRepository = new JobRepository(context));
            }
        }


        #region save

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                DbUpdateExceptionHandler(ex);
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                DbUpdateExceptionHandler(ex);
            }
        }

        private void DbUpdateExceptionHandler(DbUpdateException ex)
        {
            var builder = new StringBuilder();

            foreach (var result in ex.Entries)
            {
                builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
            }

            throw new Exception(builder.ToString(), ex);
        }

        #endregion

        #region Diposable   

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
