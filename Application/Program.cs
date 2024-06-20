using Application.Services.abstracts;
using Application.Services.concretes;
using Core.Entities.concretes;
using Core.Enums;
using DataAccess.Repositories.abstracts;
using DataAccess.Repositories.concretes;
using System;
using System.Threading.Tasks;

namespace Application
{
    class Program
    {
        public static async Task Main()
        {
            IPhoneDictionaryRepository phoneDictionaryRepository = new PhoneDictionaryRepository();
            IPhoneDictionaryService phoneDictionaryService = new PhoneDictionaryService(phoneDictionaryRepository);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz :) ");
                Console.WriteLine("*******************************************");
                Console.WriteLine("(1) Yeni Numara Kaydetmek");
                Console.WriteLine("(2) Varolan Numarayı Silmek");
                Console.WriteLine("(3) Varolan Numarayı Güncelleme");
                Console.WriteLine("(4) Rehberi Listelemek");
                Console.WriteLine("(5) Rehberde Arama Yapmak");
                Console.WriteLine("(0) Çıkış");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddNewPhoneNumber(phoneDictionaryService);
                        break;
                    case "2":
                        await DeletePhoneNumber(phoneDictionaryService);
                        break;
                    case "3":
                        await UpdatePhoneNumber(phoneDictionaryService);
                        break;
                    case "4":
                        await ListAllPhoneNumbers(phoneDictionaryService);
                        break;
                    case "5":
                        await SearchPhoneNumbers(phoneDictionaryService);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyiniz.");
                        break;
                }
            }
        }

        private static async Task AddNewPhoneNumber(IPhoneDictionaryService phoneDictionaryService)
        {
            Console.Clear();
            Console.WriteLine("Yeni Numara Kaydetmek");

            var phoneDictionary = new PhoneDictionary();
            var phoneNumber = new PhoneNumber();

            Console.Write("Ad: ");
            phoneNumber.Name = Console.ReadLine();
            Console.Write("Soyad: ");
            phoneNumber.SurName = Console.ReadLine();
            Console.Write("Telefon Numarası: ");
            phoneNumber.PhoneDigits = Console.ReadLine();
            Console.Write("Ülke Kodu (TR/US/DE): ");
            phoneNumber.Country = Enum.TryParse(Console.ReadLine(), out CountryCode country) ? country : CountryCode.TR;

            phoneDictionary.PhoneNumbers.Add(phoneNumber);

            await phoneDictionaryService.CreatePhoneDictionaryAsync(phoneDictionary);
            Console.WriteLine("Numara başarıyla kaydedildi.");
            Console.ReadLine();
        }

        private static async Task DeletePhoneNumber(IPhoneDictionaryService phoneDictionaryService)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Varolan Numarayı Silmek");
                Console.Write("Lütfen numarasını silmek istediğiniz kişinin adını ya da soyadını giriniz: ");
                var searchTerm = Console.ReadLine();

                var phoneDictionaries = await phoneDictionaryService.SearchAsync(searchTerm);

                if (phoneDictionaries.Count == 0)
                {
                    Console.WriteLine("Aradığınız krtiterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.");
                    Console.WriteLine("* Silmeyi sonlandırmak için : (1)");
                    Console.WriteLine("* Yeniden denemek için      : (2)");
                    var retryChoice = Console.ReadLine();

                    if (retryChoice == "1")
                    {
                        break;
                    }
                }
                else
                {
                    var phoneDictionary = phoneDictionaries.First();
                    Console.WriteLine($"{phoneDictionary.PhoneNumbers[0].Name} isimli kişi rehberden silinmek üzere, onaylıyor musunuz? (y/n)");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        await phoneDictionaryService.DeletePhoneDictionaryAsync(phoneDictionary);
                        Console.WriteLine("Numara başarıyla silindi.");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private static async Task UpdatePhoneNumber(IPhoneDictionaryService phoneDictionaryService)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Varolan Numarayı Güncelleme");
                Console.Write("Lütfen numarasını güncellemek istediğiniz kişinin adını ya da soyadını giriniz: ");
                var searchTerm = Console.ReadLine();

                var phoneDictionaries = await phoneDictionaryService.SearchAsync(searchTerm);

                if (phoneDictionaries.Count == 0)
                {
                    Console.WriteLine("Aradığınız krtiterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.");
                    Console.WriteLine("* Güncellemeyi sonlandırmak için    : (1)");
                    Console.WriteLine("* Yeniden denemek için              : (2)");
                    var retryChoice = Console.ReadLine();

                    if (retryChoice == "1")
                    {
                        break;
                    }
                }
                else
                {
                    var phoneDictionary = phoneDictionaries.First();
                    var phoneNumber = phoneDictionary.PhoneNumbers.First();

                    Console.Write("Yeni Ad: ");
                    phoneNumber.Name = Console.ReadLine();
                    Console.Write("Yeni Soyad: ");
                    phoneNumber.SurName = Console.ReadLine();
                    Console.Write("Yeni Telefon Numarası: ");
                    phoneNumber.PhoneDigits = Console.ReadLine();
                    Console.Write("Yeni Ülke Kodu (TR/US/DE): ");
                    phoneNumber.Country = Enum.TryParse(Console.ReadLine(), out CountryCode country) ? country : phoneNumber.Country;

                    await phoneDictionaryService.UpdatePhoneDictionaryAsync(phoneDictionary);
                    Console.WriteLine("Numara başarıyla güncellendi.");
                    Console.ReadLine();
                    break;
                }
            }
        }

        private static async Task ListAllPhoneNumbers(IPhoneDictionaryService phoneDictionaryService)
        {
            Console.Clear();
            Console.WriteLine("Rehberi Listelemek");

            var phoneDictionaries = await phoneDictionaryService.ReadAllAsync();
            Console.WriteLine("Telefon Rehberi");
            Console.WriteLine("**********************************************");
            foreach (var entry in phoneDictionaries)
            {
                foreach (var phoneNumber in entry.PhoneNumbers)
                {
                    Console.WriteLine($"isim: {phoneNumber.Name} Soyisim: {phoneNumber.SurName} Telefon Numarası: {phoneNumber.PhoneDigits}");
                }
            }
            Console.ReadLine();
        }

        private static async Task SearchPhoneNumbers(IPhoneDictionaryService phoneDictionaryService)
        {
            Console.Clear();
            Console.WriteLine("Rehberde Arama Yapmak");
            Console.WriteLine("Arama yapmak istediğiniz tipi seçiniz.");
            Console.WriteLine("**********************************************");
            Console.WriteLine("İsim veya soyisime göre arama yapmak için: (1)");
            Console.WriteLine("Telefon numarasına göre arama yapmak için: (2)");

            var searchType = Console.ReadLine();
            Console.Write("Aramak istediğiniz terim: ");
            var searchTerm = Console.ReadLine();

            List<PhoneDictionary> phoneDictionaries = null;

            if (searchType == "1")
            {
                phoneDictionaries = await phoneDictionaryService.SearchAsync(searchTerm);
            }
            else if (searchType == "2")
            {
                phoneDictionaries = await phoneDictionaryService.SearchByPhoneNumberAsync(searchTerm);
            }

            if (phoneDictionaries != null && phoneDictionaries.Count > 0)
            {
                Console.WriteLine("Arama Sonuçlarınız:");
                Console.WriteLine("**********************************************");
                foreach (var entry in phoneDictionaries)
                {
                    foreach (var phoneNumber in entry.PhoneNumbers)
                    {
                        Console.WriteLine($"isim: {phoneNumber.Name} Soyisim: {phoneNumber.SurName} Telefon Numarası: {phoneNumber.PhoneDigits}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Aradığınız kriterlere uygun veri bulunamadı.");
            }
            Console.ReadLine();
        }
    }
}
