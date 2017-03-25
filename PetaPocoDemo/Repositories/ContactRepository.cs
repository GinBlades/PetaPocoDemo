using PetaPoco;
using PetaPocoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

        // Use 'Add Reference' to add the 'System.Transactions' namespace for 'TransactionScope'
        public void Save(Contact contact)
        {
            using (var txScope = new TransactionScope())
            {
                if (contact.IsNew)
                {
                    Add(contact);
                } else
                {
                    Update(contact);
                }

                foreach(var addr in contact.Addresses.Where(a => !a.IsDeleted))
                {
                    addr.ContactId = contact.Id;
                    // Save performs the 'if' statement to determine whether Add or Update should be used.
                    // This makes an additional query to the database.
                    _db.Save(addr);
                }

                foreach(var addr in contact.Addresses.Where(a => a.IsDeleted))
                {
                    _db.Delete<Address>(addr.Id);
                }
                txScope.Complete();
            }
        }

        internal Page<Contact> GetAllPaged(int page, int size)
        {
            // You can pass additional 'WHERE' clauses to this.
            return _db.Page<Contact>(page, size, "");
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
