using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthoritySignClient.DataBase.DataBaseObjects
{
    public partial class RefAuthoritySignDocuments : IDataBaseObject
    {
        public RefAuthoritySignDocuments()
        {
            this.IsMainDefault = 0;
        }

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

        [Key, Column(@"INN"), MaxLength(15)]
        public virtual string Inn { get; set; }

        [Column(@"DATABASE_USER_NAME"), MaxLength(40)]
        public virtual string DataBaseUserName { get; set; }

        [Column(@"COMENT"), MaxLength(200)]
        public virtual string Comment { get; set; }

        [Column(@"EMCHD_ID"), MaxLength(50)]
        public virtual string EmchdId { get; set; }

        [Column(@"EMCHD_BEGIN_DATE")]
        public virtual global::System.DateTime? EmchdBeginDate { get; set; }

        [Column(@"EMCHD_END_DATE")]
        public virtual global::System.DateTime? EmchdEndDate { get; set; }

        [Column(@"IS_MAIN_DEFAULT")]
        public virtual short IsMainDefault { get; set; }

        #endregion
    }
}
