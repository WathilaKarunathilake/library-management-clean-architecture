using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.Interfaces
{
    public interface IRemoveBook
    {
        void Execute(int bookId);
    }
}
