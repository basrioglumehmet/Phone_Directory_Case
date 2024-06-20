using Core.Entities.abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.concretes
{
    public class PhoneDictionary : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        private List<PhoneNumber>? _phoneNumbers;

        public List<PhoneNumber> PhoneNumbers
        {
            get => _phoneNumbers ??= new List<PhoneNumber>();
            set => _phoneNumbers = value;
        }
    }
}
