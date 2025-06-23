using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.Features.Members
{
    public class GetMembersQuery: IRequest<IEnumerable<object>>
    {

    }
}
