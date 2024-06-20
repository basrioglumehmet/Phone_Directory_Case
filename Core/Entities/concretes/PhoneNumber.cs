using Core.Entities.abstracts;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.concretes
{
    public class PhoneNumber : IEntity
    {
        private readonly Guid id = Guid.NewGuid();
        private string? name;
        private string? surname;
        private string? phoneDigits;
        private CountryCode country = CountryCode.TR;

        public PhoneNumber() { }

        public Guid Id
        {
            get
            {
                return id;
            }
        }

        public string PhoneDigits
        {
            get
            {
                return phoneDigits ?? "";
            }
            set
            {
                phoneDigits = value;
            }
        }

        public CountryCode Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }

        public string Name
        {
            get
            {
                return name ?? "";
            }
            set
            {
                name = value;
            }
        }

        public string SurName
        {
            get
            {
                return surname ?? "";
            }
            set
            {
                surname = value;
            }
        }
    }
}
