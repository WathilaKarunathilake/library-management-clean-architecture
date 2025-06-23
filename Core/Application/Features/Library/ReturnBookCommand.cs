using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.Features.Library
{
    public class ReturnBookCommand: IRequest<Unit>
    {
        public Guid bookId { get; set; }
        public Guid memberId { get; set; }
    }
}
