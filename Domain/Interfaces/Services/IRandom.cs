using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IRandom<T>
    {
        Task<T> Generate();

        Task<IEnumerable<T>> Generate(int count);
    }

}
