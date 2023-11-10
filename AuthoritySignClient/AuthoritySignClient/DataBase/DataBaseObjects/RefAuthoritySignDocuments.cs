using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthoritySignClient.DataBase.DataBaseObjects
{
    public partial class RefAuthoritySignDocuments : IDataBaseObject
    {
        #region Properties
        [Key]
        public virtual decimal IdCustomer { get; set; }

        [Column(@"SURNAME"), MaxLength(60)]
        public virtual string Surname { get; set; }

        [Column(@"NAME"), MaxLength(60)]
        public virtual string Name { get; set; }

        [Column(@"PATRONIMYC_SURNAME"), MaxLength(60)]
        public virtual string PatronymicSurname { get; set; }

        [Column(@"POSITION"), MaxLength(128)]
        public virtual string Position { get; set; }

        [Column(@"INN"), MaxLength(15)]
        public virtual string Inn { get; set; }

        #endregion
    }
}
