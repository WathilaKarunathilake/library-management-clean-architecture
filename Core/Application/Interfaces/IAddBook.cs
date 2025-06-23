using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.Interfaces
{
    public interface IAddBook
    {
        Book Execute(BookDTO bookDto);
    }
}
