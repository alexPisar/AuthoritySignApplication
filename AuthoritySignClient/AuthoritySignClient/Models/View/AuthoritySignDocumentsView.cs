using AuthoritySignClient.DataBase.DataBaseObjects;

namespace AuthoritySignClient.Models.View
{
    public class AuthoritySignDocumentsView
    {
        public RefCustomer Customer { get; set; }
        public RefAuthoritySignDocuments AuthoritySignDocuments { get; set; }
    }
}
