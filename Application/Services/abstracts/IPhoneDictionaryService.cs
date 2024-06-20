using Core.Entities.concretes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.abstracts
{
    public interface IPhoneDictionaryService
    {
        Task CreatePhoneDictionaryAsync(PhoneDictionary phoneDictionary);
        Task<List<PhoneDictionary>> ReadAllAsync();
        Task<PhoneDictionary> GetPhoneDictionaryByIdAsync(Guid id);
        Task DeletePhoneDictionaryAsync(PhoneDictionary phoneDictionary);
        Task UpdatePhoneDictionaryAsync(PhoneDictionary phoneDictionary);
        Task<List<PhoneDictionary>> SearchAsync(string searchTerm);
        Task<List<PhoneDictionary>> SearchByPhoneNumberAsync(string phoneNumber);
    }
}
