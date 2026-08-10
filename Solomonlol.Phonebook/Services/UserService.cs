using AutoMapper;
using Backend.Exceptions;
using Backend.Interfaces;
using Backend.Models;
using Backend.Models.Dto;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{
    public class UserService : IService<User, UserDto>
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
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

        public async Task CreateAsync(CreateUserDto item, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = _mapper.Map<User>(item);
                if(!string.IsNullOrEmpty(user.Email))
                    user.EmailPasswordHash = _passwordHasher.HashPassword(user, item.EmailPassword);
                user.LoginPasswordHash = _passwordHasher.HashPassword(user, item.Password);
                
                await _unitOfWork.Users.Create(user, cancellationToken);
                await _unitOfWork.SaveAsync(cancellationToken);
            }
            catch(AutoMapperMappingException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }
        public async Task CreateAsync(UserDto item, CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException("Use CreateAsync(CreateUserDto) instead");
        }

        public async Task UpdateAsync(int id, UserDto dto, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.Users.Get(id, cancellationToken);
            if (user == null)
                throw new NotFoundException();

            _mapper.Map(dto, user);

            await _unitOfWork.Users.Update(user, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Users.Delete(id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
        public async Task DeleteAsync(string login, string password, CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.Users.GetByLogin(login, cancellationToken);
            if (user is null)
                throw new NotFoundException("User not found");

            var result = _passwordHasher.VerifyHashedPassword(user, user.LoginPasswordHash, password);

            if (result == PasswordVerificationResult.Failed)
                throw new ValidationException("Invalid password");

            await DeleteAsync(user.Id, cancellationToken);
        }
    }
}
