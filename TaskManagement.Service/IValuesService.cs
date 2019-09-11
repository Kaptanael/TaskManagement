using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Dto.Value;

namespace TaskManagement.Service
{
    public interface IValueService
    {
        Task<bool> AddAsync(ValueForCreateDto valueForCreateDto, CancellationToken cancellationToken = default(CancellationToken));

        bool Update(ValueForUpdateDto valueForUpdateDto);

        bool Delete(ValueForDeleteDto valueForDeleteDto);

        Task<ValueForListDto> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<ValueForListDto>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> IsDuplicateAsync(string name, CancellationToken cancellationToken = default(CancellationToken));
    }
}
