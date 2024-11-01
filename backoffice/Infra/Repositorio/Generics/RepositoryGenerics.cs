using Domain.Interfaces.Generics;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Infra.Repositorio.Generics
{
    public class RepositoryGenerics<T> : InterfaceGeneric<T>, IDisposable where T : class
    {
        //private readonly DbContextOptions<ContextBase> _OptionsBuilder;


        //public RepositoryGenerics()
        //{
        //    _OptionsBuilder = new DbContextOptions<ContextBase>();
        //}

        //public async Task AddAsync(T Objeto)
        //{
        //    using (var data = new ContextBase(_OptionsBuilder))
        //    {
        //        await data.Set<T>().AddAsync(Objeto);
        //        await data.SaveChangesAsync();
        //    }
        //}

        //public async Task DeleteAsync(T Objeto)
        //{
        //    using (var data = new ContextBase(_OptionsBuilder))
        //    {
        //        data.Set<T>().Remove(Objeto);
        //        await data.SaveChangesAsync();
        //    }
        //}

        //public async Task<T> GetEntityByIdAsync(int Id)
        //{
        //    using (var data = new ContextBase(_OptionsBuilder))
        //    {
        //        return await data.Set<T>().FindAsync(Id);
        //    }
        //}

        //public async Task<List<T>> ListAsync()
        //{
        //    using (var data = new ContextBase(_OptionsBuilder))
        //    {
        //        return await data.Set<T>().ToListAsync();
        //    }
        //}

        //public async Task UpdateAsync(T Objeto)
        //{
        //    using (var data = new ContextBase(_OptionsBuilder))
        //    {
        //        data.Set<T>().Update(Objeto);
        //        await data.SaveChangesAsync();
        //    }
        //}


        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);



        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion

    }
}
