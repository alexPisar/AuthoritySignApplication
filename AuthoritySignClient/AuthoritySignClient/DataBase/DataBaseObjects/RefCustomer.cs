using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthoritySignClient.DataBase.DataBaseObjects
{
    public class RefCustomer : IDataBaseObject
    {
        #region Properties
        [Key]
        public virtual decimal Id { get; set; }

        [Column(@"PHONES")]
        public virtual string Phones { get; set; }

        [Column(@"POSTAL_ADDRESS")]
        public virtual string PostalAddress { get; set; }

        [Column(@"NAME")]
        public virtual string Name { get; set; }

        [Column(@"JURIDICAL_ADDRESS")]
        public virtual string Address { get; set; }

        [Column(@"INN")]
        public virtual string Inn { get; set; }

        [Column(@"KPP")]
        public virtual string Kpp { get; set; }

        [Column(@"OKPO")]
        public virtual string Okpo { get; set; }

        [Column(@"OKONH")]
        public virtual string Okonh { get; set; }

        [Column(@"DIRECTOR")]
        public virtual string Director { get; set; }
        #endregion
    }
}
