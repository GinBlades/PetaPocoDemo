using PetaPocoDemo.Models;
using PetaPocoDemo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetaPocoDemo
{
    class Program
    {
        // By adding your connection string to the 'Database.tt', just saving the
        // file will generate models based on your database.
        static void Main(string[] args)
        {
            var repo = new ContactRepository();

            var newContact = new Contact
            {
                FirstName = "Sven",
                LastName = "Martin",
                Company = "Home",
                Email = "sven@example.com",
                Title = "Supreme Pizza"
            };
            // Requires a State with ID of 1 in the database
            var newAddress = new Address
            {
                AddressType = "Home",
                StreetAddress = "123 The Street",
                City = "Madison",
                StateId = 1,
                PostalCode = "54321"
            };
            newContact.Addresses.Add(newAddress);

            repo.Save(newContact);

            var contacts = repo.GetAll();
            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.FirstName} - {contact.Title}");
            }

            Console.WriteLine($"Newest: {repo.Find(newContact.Id).FirstName}");
            Console.WriteLine($"Total contacts: {repo.GetAll().Count}");
            repo.Remove(newContact.Id);
            Console.WriteLine($"Remaining contacts: {repo.GetAll().Count}");
        }
    }
}
