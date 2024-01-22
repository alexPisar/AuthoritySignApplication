using AuthoritySignClient.DataBase.DataBaseObjects;

namespace AuthoritySignClient.Models.View
{
    public class AuthoritySignDocumentsView
    {
        public RefCustomer Customer { get; set; }
        public RefAuthoritySignDocuments AuthoritySignDocuments { get; set; }

        public bool IsMainDefault {
            get {
                if (AuthoritySignDocuments != null)
                    return AuthoritySignDocuments.IsMainDefault != 0;

                return false;
            }
        }
    }
}
