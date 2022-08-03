using System.Collections.Generic;
using MongoDB.Driver;

namespace SettlementBookingSystem.Application.Common;

public class PagingCriteria<T>
{
    public PagingCriteria()
    {
        Keywords = new List<string>();
    }

    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 0;
    public SortField SortField { get; set; }
    public IEnumerable<string> Keywords { get; set; }
    public string CompanyId { get; set; }
    public string CompanyDomain { get; set; }
    public string UserId { get; set; }
    public string UserRole { get; set; }
    public bool IsAll { get; set; }

    /*public SortDefinition<T> SortBy
    {
        get
        {
            if (string.IsNullOrWhiteSpace(SortField.Field))
            {
                return null;
            }

            return SortField.Direction == SortDirection.Asc ? Builders<T>.Sort.Ascending(SortField.Field) : Builders<T>.Sort.Descending(SortField.Field);
        }
    }*/
    
    /*
     * req.params.page, req.params.count, req.params.filterValue, req.params.type,
                req.params.sortField, req.params.isDesc, req.params.isPublished, req.params.isAll, req.params.tags, req.params.isEcommerce,
                req.params.inGlobal
     */
}