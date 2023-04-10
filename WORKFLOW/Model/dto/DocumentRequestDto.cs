
namespace WORKFLOW.Model.dto
{
    public class DocumentRequestDto
    {
        public string docNumber { get; set; } = string.Empty;
        public string docType { get; set; } = string.Empty;
        public DateTime docDate { get; set; }
        public string creator { get; set; } = string.Empty;
        public string module { get; set; } = string.Empty;
        public UserAccess? userAccess { get; set; }
        public List<DocumentDetail>? documentDetails { get; set; }
    }

    public class DocumentDetail
    {
        public string docNumber { get; set; } = string.Empty;
        public int lineNumber { get; set; }
        public string item { get; set; } = string.Empty;
        public decimal qty { get; set; }
    }

    public class UserAccess
    {
        public string userName { get; set; } = string.Empty;
        public string dept { get; set; } = string.Empty;
        public string headUser { get; set; } = string.Empty;
    }
}
