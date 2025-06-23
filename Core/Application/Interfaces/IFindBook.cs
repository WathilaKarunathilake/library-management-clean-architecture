using LibraryManagementCleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.Interfaces
{
    public interface IFindBook
    {
        Book Execute(int bookId);
    }
}
