namespace SettlementBookingSystem.Application.Features.Articles.Models;

public class UpdateArticleRequest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Description { get; set; }
    
}