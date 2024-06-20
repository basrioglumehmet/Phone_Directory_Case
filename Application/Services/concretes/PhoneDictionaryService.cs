using Application.Services.abstracts;
using Core.Entities.concretes;
using DataAccess.Repositories.abstracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.concretes
{
    public class PhoneDictionaryService : IPhoneDictionaryService
    {
        private readonly IPhoneDictionaryRepository _phoneDictionaryRepository;

        public PhoneDictionaryService(IPhoneDictionaryRepository phoneDictionaryRepository)
        {
            _phoneDictionaryRepository = phoneDictionaryRepository;
        }

        public async Task CreatePhoneDictionaryAsync(PhoneDictionary phoneDictionary)
        {
            await _phoneDictionaryRepository.AddAsync(phoneDictionary);
        }

        public async Task<List<PhoneDictionary>> ReadAllAsync()
        {
            return await _phoneDictionaryRepository.GetAllAsync(pd => true);
        }

        public async Task<PhoneDictionary> GetPhoneDictionaryByIdAsync(Guid id)
        {
            return await _phoneDictionaryRepository.GetFirstAsync(pd => pd.Id == id);
        }

        public async Task DeletePhoneDictionaryAsync(PhoneDictionary phoneDictionary)
        {
            await _phoneDictionaryRepository.DeleteAsync(phoneDictionary);
        }

        public async Task UpdatePhoneDictionaryAsync(PhoneDictionary phoneDictionary)
        {
            await _phoneDictionaryRepository.UpdateAsync(phoneDictionary);
        }

        public async Task<List<PhoneDictionary>> SearchAsync(string searchTerm)
        {
            return await _phoneDictionaryRepository.GetAllAsync(pd =>
                pd.PhoneNumbers.Any(pn =>
                    pn.Name.Contains(searchTerm) || pn.SurName.Contains(searchTerm)));
        }

        public async Task<List<PhoneDictionary>> SearchByPhoneNumberAsync(string phoneNumber)
        {
            return await _phoneDictionaryRepository.GetAllAsync(pd =>
                pd.PhoneNumbers.Any(pn =>
                    pn.PhoneDigits.Contains(phoneNumber)));
        }
    }
}
