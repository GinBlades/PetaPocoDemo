using PetaPoco;
using PetaPocoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetaPocoDemo.Repositories
{
    class ContactRepository
    {
        private Database _db = new Database("ContactsDb");
        public List<Contact> GetAll()
        {
            // return _db.Query<Contact>("SELECT * FROM Contacts").ToList();
            // Requires data annotations to get the proper table name and properties
            return _db.Query<Contact>("").ToList();
        }

        public Contact Add(Contact contact)
        {
            // If you don't have annotations, specify table name and primary key here
            // _db.Insert("Contacts", "Id", contact);
            _db.Insert(contact);
            return contact;
        }

        public Contact Find(int id)
        {
            return _db.SingleOrDefault<Contact>("WHERE Id = @0", id);
        }

        public Contact Update(Contact contact)
        {
            _db.Update(contact);
            return contact;
        }

        public void Remove(int id)
        {
            // You can also pass in a full object
            _db.Delete<Contact>(id);
        }

        public void Save(Contact newContact)
        {
            //using (var txScope = new TransactionScope())
            //{

            //}
        }

        public Contact GetFullContact(int id)
        {
            var contact = Find(id);
            var addresses = _db.Query<Address>("WHERE ContactId = @0", id).ToList();

            if (contact != null && addresses != null)
            {
                contact.Addresses.AddRange(addresses);
            }

            return contact;
        }
    }
}
