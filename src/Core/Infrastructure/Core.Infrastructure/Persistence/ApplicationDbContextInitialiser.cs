using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        await AddInitial(_context.Vendors, new Vendor("TestVendor")
        {
            ShopOwnerId = "dddd",
            City = "Berlin",
            CorporateEmail = "andreas@mail.com",
            Street = "First Street",
            StreetNo = "1",
            PLZ = "10333"
        });

        await AddInitial(_context.SalesInfos, new SalesInfo() { RatingScore = new int[5] { 20, 15, 5, 5, 3 }, NumberOfSales = 48 });

        var salesInfo = await _context.SalesInfos.FirstAsync();
        await AddInitial(_context.Ratings, new Rating()
        {
            UserId = "34",
            UserFullName = "SomeUser",
            SalesInfoId = salesInfo.Id,
            RatingValue = 1,
            Description = "This sucked hard"
        });


        var vendor = await _context.Vendors.FirstAsync();
        if (!_context.Products.Any())
        {
            _context.Products.AddRange(
                new Product() {Name = "Product1", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et just", CreationDate = DateTime.Now.AddDays(-4), MainCategory = MainCategory.Kunst, SubCategory = nameof(SubCategoryArt.Bilder), Thumbnail = new Uri("/images/thumbnails/01.jpg", UriKind.Relative), VendorId = vendor.Id, SalesInfoId = salesInfo.Id, SalesInfo = null },
                //new Product() { Id = 2, Description = "Lorem ipsum dolor sit amet, sed diam voluptua. At vero eos et accusam et just", CreationDate = DateTime.Now.AddDays(-62), Category = Category.Art, Thumbnail = new Uri("/images/thumbnails/02.jpg", UriKind.Relative), VendorId = vendor.Id, SalesInfoId = salesInfo.Id },
                new Product() { Name = "Product3", Description = "sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et just", CreationDate = DateTime.Now.AddDays(-32), MainCategory = MainCategory.Digitales, SubCategory = nameof(SubCategoryDigital.Karten), Thumbnail = new Uri("/images/thumbnails/03.jpg", UriKind.Relative), VendorId = vendor.Id, SalesInfoId = salesInfo.Id, SalesInfo = null },
                new Product() {  Name = "Product4", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et just", CreationDate = DateTime.Now, MainCategory = MainCategory.Fotographie, SubCategory = nameof(SubCategoryFoto.Locations), Thumbnail = new Uri("/images/thumbnails/04.jpg", UriKind.Relative), VendorId = vendor.Id, SalesInfoId = salesInfo.Id, SalesInfo = null },
                //new Product() { Id = 5, Name = "Product5", Description = "Lorem ipsum dolor sit amet, sed diam voluptua. At vero eos et accusam et just", CreationDate = DateTime.Now.AddDays(-64), Thumbnail = new Uri("/images/thumbnails/01.jpg", UriKind.Relative), VendorId = vendor.Id },
                new Product() {  Name = "Product6", Description = "sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et just", CreationDate = DateTime.Now.AddDays(-33), MainCategory = MainCategory.Fotographie, SubCategory = nameof(SubCategoryFoto.Portraits), Thumbnail = new Uri("/images/thumbnails/01.jpg", UriKind.Relative), VendorId = vendor.Id, SalesInfoId = salesInfo.Id, SalesInfo = null },
                new Product() { Name = "Product7", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et just", CreationDate = DateTime.Now, MainCategory = MainCategory.Digitales, SubCategory = nameof(SubCategoryDigital.Flyer), Thumbnail = new Uri("/images/thumbnails/01.jpg", UriKind.Relative), VendorId = vendor.Id, SalesInfoId = salesInfo.Id, SalesInfo = null },
                new Product() { Name = "Product8", Description = "sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et just", CreationDate = DateTime.Now.AddDays(-32), MainCategory = MainCategory.Kunst, SubCategory = nameof(SubCategoryArt.Dekoration), Thumbnail = new Uri("/images/thumbnails/01.jpg", UriKind.Relative), VendorId = vendor.Id, SalesInfoId = salesInfo.Id, SalesInfo = null },
                new Product() {Name = "Product9", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et just", CreationDate = DateTime.Now.AddDays(-10), MainCategory = MainCategory.Musik, SubCategory = nameof(SubCategoryMusik.Events), Thumbnail = new Uri("/images/thumbnails/01.jpg", UriKind.Relative), VendorId = vendor.Id, SalesInfoId = salesInfo.Id, SalesInfo = null },
                new Product() {  Name = "Product10", Description = "Lorem ipsum dolor sit amet, sed diam voluptua. At vero eos et accusam et just", CreationDate = DateTime.Now.AddDays(-203), MainCategory = MainCategory.Digitales, SubCategory = nameof(SubCategoryDigital.Karten), Thumbnail = new Uri("/images/thumbnails/01.jpg", UriKind.Relative), VendorId = vendor.Id, SalesInfoId = salesInfo.Id, SalesInfo = null },
                new Product() { Name = "Product11", Description = "sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et just", CreationDate = DateTime.Now.AddDays(-3), MainCategory = MainCategory.Musik, SubCategory = nameof(SubCategoryMusik.Online), Thumbnail = new Uri("/images/thumbnails/01.jpg", UriKind.Relative), VendorId = vendor.Id, SalesInfoId = salesInfo.Id, SalesInfo = null },
                new Product() {  Name = "Product12", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et just", CreationDate = DateTime.Now.AddDays(-1), MainCategory = MainCategory.Kunst, SubCategory = nameof(SubCategoryArt.Graffiti), Thumbnail = new Uri("/images/thumbnails/01.jpg", UriKind.Relative), VendorId = vendor.Id, SalesInfoId = salesInfo.Id, SalesInfo = null }
                );

            await _context.SaveChangesAsync();
        }

    }

    private async Task AddInitial<T>(DbSet<T> originalSet, T value) where T : class
    {
        if (!originalSet.Any())
        {
            originalSet.Add(value);

            await _context.SaveChangesAsync();
        }
    }
}
