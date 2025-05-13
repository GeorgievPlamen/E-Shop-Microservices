using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
        {
            return;
        }

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() =>
    [
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "IPhone X",
            Description ="dummy data",
            ImageFile = "product-1.png",
            Price = 950.00M,
            Category = ["Smart Phone"]
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Samsung 10",
            Description ="dummy data 2",
            ImageFile = "product-2.png",
            Price = 850.00M,
            Category = ["Smart Phone"]
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "LG TV",
            Description ="dummy data 3",
            ImageFile = "product-3.png",
            Price = 600.00M,
            Category = ["TV"]
        }
    ];
}
