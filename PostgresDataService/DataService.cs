using DataLayer;
using Microsoft.EntityFrameworkCore;


namespace PostgresDataService;
public class DataService : IDataService
{
    public Category CreateCategory(string name, string description)
    {
        var db = new NorthwindContext();
        var id = db.Categories.Max(x => x.Id) + 1;

        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };

        db.Categories.Add(category);

        db.SaveChanges();

        return category;

    }

    public int NumberOfCategories()
    {
        var db = new NorthwindContext();
        return db.Categories.Count();
    }


    public bool UpdateCategory(Category category)
    {
        var db = new NorthwindContext();

        db.Update(category);

        return db.SaveChanges() > 0;
    }

    public bool DeleteCategory(int id)
    {
        var db = new NorthwindContext();

        var category = db.Categories.Find(id);

        if (category == null)
        {
            return false;
        }

        db.Categories.Remove(category);
        //db.Remove(category);

        return db.SaveChanges() > 0;
    }

    public IList<Category> GetCategories(int page, int pageSize)
    {
        var db = new NorthwindContext();
        return db.Categories
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public Category? GetCategory(int id)
    {
        var db = new NorthwindContext();
        return db.Categories.Find(id);
    }

    public IList<Product> GetProducts(int page, int pageSize)
    {
        var db = new NorthwindContext();
        return db.Products
            .Include(x => x.Category)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int NumberOfProducts()
    {
        var db = new NorthwindContext();
        return db.Products.Count();
    }

    public Product? GetProduct(int id)
    {
        var db = new NorthwindContext();
        return db.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
    }
}
