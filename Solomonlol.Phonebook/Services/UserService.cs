using AutoMapper;
using Backend.Exceptions;
using Backend.Interfaces;
using Backend.Models;
using Backend.Models.Dto;

namespace Backend.Services
{
    internal class UserService : IService<User, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto?> GetById(int id, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.Users.Get(id, cancellationToken);
            var dto = new UserDto();
            _mapper.Map(user, dto);
            return dto;
        }

        public async Task<IEnumerable<UserDto>> GetList(CancellationToken cancellationToken = default)
        {
            var userList = await _unitOfWork.Users.GetList(cancellationToken: cancellationToken);
            return userList.Select(user => _mapper.Map<UserDto>(user));
        }

        public async Task CreateAsync(UserDto item, CancellationToken cancellationToken = default)
        {
            var user = new User();
            _mapper.Map(item, user);
            await _unitOfWork.Users.Create(user, cancellationToken);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, UserDto dto, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.Users.Get(id, cancellationToken);
            if (user == null)
                throw new NotFoundException();

            _mapper.Map(dto, user);

            await _unitOfWork.Users.Update(user, cancellationToken);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Users.Delete(id, cancellationToken);
            await _unitOfWork.SaveAsync();
        }
    }
}
