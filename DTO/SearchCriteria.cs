using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Principal;

namespace Stock_Explorer.DTO
{
    public class SearchCriteria
    {
        public string? Name { get; set; }
        public string Date { get; set; }
    }
}
