﻿using DataLayer;
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

    public IList<Category> GetCategories()
    {
        var db = new NorthwindContext();
        return db.Categories.ToList();
    }

    public Category? GetCategory(int id)
    {
        var db = new NorthwindContext();
        return db.Categories.Find(id);
    }

    public IList<Product> GetProducts()
    {
        var db = new NorthwindContext();
        return db.Products.Include(x => x.Category).ToList();
    }
}
