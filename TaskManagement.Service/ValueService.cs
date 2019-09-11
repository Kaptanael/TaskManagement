using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Data.UnitOfWork;
using TaskManagement.Dto.Value;
using TaskManagement.Model;

namespace TaskManagement.Service
{
    public class ValueService : IValueService
    {
        private readonly IUnitOfWork _uow;

        public ValueService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> AddAsync(ValueForCreateDto valueForCreateDto, CancellationToken cancellationToken = default(CancellationToken))
        {
            var valueToCreate = new Value
            {
                Name = valueForCreateDto.Name
            };

            await _uow.Values.AddAsync(valueToCreate);
            return await _uow.SaveAsync(cancellationToken) > 0 ? true : false;
        }

        public bool Update(ValueForUpdateDto valueForUpdateDto)
        {
            var valueToUpdate = new Value
            {
                Id = valueForUpdateDto.Id,
                Name = valueForUpdateDto.Name
            };            

            var updatedValue = _uow.Values.Update(valueToUpdate);
            return _uow.Save() > 0 ? true : false;
        }

        public bool Delete(ValueForDeleteDto valueForDeleteDto)
        {
            var valueToDelete = new Value
            {
                Id = valueForDeleteDto.Id,
                Name = valueForDeleteDto.Name
            };

            var deletedValue = _uow.Values.Delete(valueToDelete);
            return _uow.Save() > 0 ? true : false;
        }

        public async Task<IEnumerable<ValueForListDto>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            List<ValueForListDto> valueForListDtos = null;
            var values = await _uow.Values.GetAllAsync();

            if (values != null)
            {
                valueForListDtos = new List<ValueForListDto>();
                foreach (var value in values)
                {
                    ValueForListDto valueForListDto = new ValueForListDto();
                    valueForListDto.Id = value.Id;
                    valueForListDto.Name = value.Name;
                    valueForListDtos.Add(valueForListDto);
                }
            }

            return valueForListDtos;
        }

        public async Task<ValueForListDto> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var value = await _uow.Values.GetByIdAsync(id);
            ValueForListDto valueForListDto = null;

            if (value != null)
            {
                valueForListDto = new ValueForListDto();
                valueForListDto.Id = value.Id;
                valueForListDto.Name = value.Name;
            }

            return valueForListDto;
        }

        public async Task<bool> IsDuplicateAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var value = await _uow.Values.IsDuplicateAsync(name);
            return value;
        }
    }
}
