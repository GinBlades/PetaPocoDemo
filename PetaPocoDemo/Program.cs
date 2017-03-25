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
        static void Main(string[] args)
        {
            var repo = new ContactRepository();

            var newContact = repo.Add(new Contact
            {
                FirstName = "Sven",
                LastName = "Martin",
                Company = "Home",
                Email = "sven@example.com",
                Title = "Supreme Pizza"
            });
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
