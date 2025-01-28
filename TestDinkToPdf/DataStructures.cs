namespace HelloMigraDoc;

public class OfferMainInfoResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? ContactFullName { get; set; }

    public string? CreateUserFullName { get; set; }

    public OfferStatus Status { get; set; }
    public string StatusDescription { get; set; } = string.Empty;

    public int ExtraServicesCount { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? AcceptedOrRejectedAt { get; set; }
    public bool IsEditable { get; set; }

}

public class OfferArticleItemResponse
{
    public int Id { get; set; }
    public string Code { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal[] Discounts { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal NetPrice { get; set; }
}

public class OfferServiceListResponse
{
    public int Id { get; set; }
    public BusinessCentralServiceType Type { get; set; }
    public string TypeDescription { get; set; }
    public string BusinessCentralCode { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public int OfferId { get; set; }

}

public class OfferServicesSummaryResponse
{
    public List<OfferServiceListResponse> Services { get; set; } = null!;
    public decimal TotalAmount { get; set; }
}

public class OfferArticleSummaryResponse
{
    public List<OfferArticleItemResponse> Articles { get; set; }

    public decimal TotalAmount { get; set; }
}

public enum OfferStatus
{
    OPENED = 0,
    SENT = 1,
    ACCEPTED = 2,
    REJECTED = 3,
}

public enum BusinessCentralServiceType
{
    Transport = 0,
    Service = 1,
}