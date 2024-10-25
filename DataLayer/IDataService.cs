namespace DataLayer;
public interface IDataService
{
    IList<Category> GetCategories(int page, int pageSize);
    int NumberOfCategories();
    Category GetCategory(int id);
    Category CreateCategory(string name, string description);

    bool UpdateCategory(Category category);

    bool DeleteCategory(int id);

    IList<Product> GetProducts(int page, int pageSize);

    int NumberOfProducts();

    Product? GetProduct(int id);
}
