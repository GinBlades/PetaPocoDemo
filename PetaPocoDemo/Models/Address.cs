using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetaPocoDemo.Models
{
    [TableName("Addresses")]
    [PrimaryKey("Id")]
    public class Address
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string AddressType { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string PostalCode { get; set; }

        [Ignore]
        public bool IsDeleted { get; set; }

        [Ignore]
        internal bool IsNew {
            get {
                return Id == default(int);
            }
        }
    }
}
