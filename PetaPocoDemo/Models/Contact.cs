using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace PetaPocoDemo.Models
{
    // Convention looks for table matching class name
    [TableName("Contacts")]
    [PrimaryKey("Id")]
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }

        [Ignore]
        public List<Address> Addresses { get; set; }

        [Ignore]
        public bool IsNew {
            get {
                return Id == default(int);
            }
        }
    }
}
