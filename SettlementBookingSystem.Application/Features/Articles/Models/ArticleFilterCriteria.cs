using System;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;

namespace SettlementBookingSystem.Application.Features.Articles.Models;

public class ArticleFilterCriteria: PagingCriteria<Article>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}