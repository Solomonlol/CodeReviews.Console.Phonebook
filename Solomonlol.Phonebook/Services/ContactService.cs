using AutoMapper;
using Backend.Exceptions;
using Backend.Interfaces;
using Backend.Models;
using Backend.Models.Dto;

namespace Backend.Services
{
    public class ContactService : IService<Contact, ContactDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CurrentUserService _currentUserService;
        public ContactService(IUnitOfWork unitOfWork, IMapper mapper, CurrentUserService currentUserService )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }
        public async Task CreateAsync(ContactDto item, CancellationToken cancellationToken = default)
        {
            var contact = new Contact();
            _mapper.Map(item, contact);
            contact.UserId = _currentUserService.CurrentUser.Id;
            await _unitOfWork.Contacts.Create(contact);
            await _unitOfWork.SaveAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Contacts.Delete(id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
        public async Task DeleteAsync(ContactDto dto, CancellationToken cancellationToken = default)
        {
            var contact = await _unitOfWork.Contacts.GetByPhoneNumberAsync(dto.PhoneNumber);
            if (contact != null)
                await DeleteAsync(contact.Id, cancellationToken);
            else throw new NotFoundException("Contact not exists");
        }

        public async Task<IEnumerable<ContactDto>> GetList(CancellationToken cancellationToken = default)
        {
            var contactList = await _unitOfWork.Contacts.GetList(cancellationToken: cancellationToken);

            return contactList.Select(contact => _mapper.Map<ContactDto>(contact)).Where(s=>s.UserId==_currentUserService.CurrentUser.Id);
        }

        public async Task<ContactDto?> GetById(int id, CancellationToken cancellationToken = default)
        {
            var contact = await _unitOfWork.Contacts.Get(id, cancellationToken);

            var dto = new ContactDto();
            _mapper.Map(contact, dto);

            return dto;
        }

        public async Task UpdateAsync(ContactDto dto, CancellationToken cancellationToken = default)
        {
            var contact = await _unitOfWork.Contacts.GetByPhoneNumberAsync(dto.PhoneNumber, cancellationToken);
            if (contact == null)
            {
                throw new NotFoundException($"Contact was not found");
            }
            
            _mapper.Map(dto, contact);

            await _unitOfWork.Contacts.Update(contact);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
