namespace MigraDocExample;

public class Data
{
    public static readonly OfferMainInfoResponse MainInfo = new()
    {
                Id = 14,
                Name = "Test Offerta",
                ContactFullName = "Test Company S.p.a.",
                CreateUserFullName = "Admin User",
                Status = OfferStatus.SENT, // Assuming 1 corresponds to SENT in OfferStatus enum
                StatusDescription = "Inviata",
                ExtraServicesCount = 5,
                CreatedAt = DateTime.Parse("2024-12-19T13:43:31.866768Z"),
                SentAt = DateTime.Parse("2025-01-20T07:59:05.256454Z"),
                AcceptedOrRejectedAt = null,
                IsEditable = true
            };
            
            public static readonly OfferArticleSummaryResponse Articles = new()
            {
                Articles = new List<OfferArticleItemResponse>
                {
                    new OfferArticleItemResponse
                    {
                        Id = 1,
                        Code = "000001",
                        Description = "T RIN",
                        Quantity = 90,
                        Price = (decimal)0.452,
                        Discounts = new decimal[]{},
                        TotalPrice = (decimal)40.68,
                        NetPrice = (decimal)0.452
                    },
                    new OfferArticleItemResponse
                    {
                        Id = 2,
                        Code = "000002",
                        Description = "INTOCALX LIGHT",
                        Quantity = 20,
                        Price = (decimal)0.411,
                        Discounts = new decimal[] { (decimal)0.1 },
                        TotalPrice = (decimal)7.398,
                        NetPrice = (decimal)0.3699
                    }
                },
                TotalAmount = (decimal)48.078
            };

            public static readonly OfferServicesSummaryResponse Services =  new()
            {
                Services = new List<OfferServiceListResponse>
                {
                    new OfferServiceListResponse
                    {
                        Id = 3,
                        Type = 0,
                        TypeDescription = "Trasporto",
                        BusinessCentralCode = "11234",
                        Description = "Trasporto autotreno 300Km",
                        Quantity = 1,
                        Price = 500,
                        TotalPrice = 500,
                        OfferId = 14
                    },
                    new OfferServiceListResponse
                    {
                        Id = 1,
                        Type = 0,
                        TypeDescription = "Trasporto",
                        BusinessCentralCode = "11234",
                        Description = "Trasporto autotreno 300Km",
                        Quantity = 3,
                        Price = 500,
                        TotalPrice = 1500,
                        OfferId = 14
                    },
                    new OfferServiceListResponse
                    {
                        Id = 2,
                        Type = 0,
                        TypeDescription = "Servizio",
                        BusinessCentralCode = "11235",
                        Description = "Consegna macchina intonacatrice",
                        Quantity = 1,
                        Price = 60,
                        TotalPrice = 60,
                        OfferId = 14
                    }
                },
                TotalAmount = 2060
            };
}