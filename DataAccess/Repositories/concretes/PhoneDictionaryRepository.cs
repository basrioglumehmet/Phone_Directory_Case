using Core.Entities.concretes;
using DataAccess.Repositories.abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories.concretes
{
    public class PhoneDictionaryRepository : IPhoneDictionaryRepository
    {
        private readonly List<PhoneDictionary> _phoneDictionaries = new List<PhoneDictionary>();
        private readonly object _lockObject = new object();

        public async Task<PhoneDictionary> AddAsync(PhoneDictionary entity)
        {
            lock (_lockObject)
            {
                _phoneDictionaries.Add(entity);
            }
            return await Task.FromResult(entity);
        }

        public async Task<PhoneDictionary> DeleteAsync(PhoneDictionary entity)
        {
            PhoneDictionary itemToDelete;
            lock (_lockObject)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} Thread Çalışıyor : {Thread.CurrentThread.ManagedThreadId}");
                itemToDelete = _phoneDictionaries.FirstOrDefault(x => x.Id == entity.Id);

                if (itemToDelete != null)
                {
                    _phoneDictionaries.Remove(itemToDelete);
                }
                else
                {
                    Console.WriteLine($"Item with Id {entity.Id} not found.");
                }

                Console.WriteLine($"{Thread.CurrentThread.Name} Thread Sonlandırıldı : {Thread.CurrentThread.ManagedThreadId}");
            }

            return await Task.FromResult(itemToDelete);
        }

        public async Task<List<PhoneDictionary>> GetAllAsync(Expression<Func<PhoneDictionary, bool>> predicate)
        {
            List<PhoneDictionary> result;
            lock (_lockObject)
            {
                result = _phoneDictionaries.AsQueryable().Where(predicate).ToList();
            }
            return await Task.FromResult(result);
        }

        public async Task<PhoneDictionary> GetFirstAsync(Expression<Func<PhoneDictionary, bool>> predicate)
        {
            PhoneDictionary result;
            lock (_lockObject)
            {
                result = _phoneDictionaries.AsQueryable().FirstOrDefault(predicate);
            }
            return await Task.FromResult(result);
        }

        public async Task<PhoneDictionary> UpdateAsync(PhoneDictionary entity)
        {
            lock (_lockObject)
            {
                var existingEntity = _phoneDictionaries.FirstOrDefault(x => x.Id == entity.Id);
                if (existingEntity != null)
                {
                    existingEntity.PhoneNumbers = entity.PhoneNumbers;
                }
                else
                {
                    throw new KeyNotFoundException($"Entity with Id {entity.Id} not found");
                }
            }
            return await Task.FromResult(entity);
        }
    }
}