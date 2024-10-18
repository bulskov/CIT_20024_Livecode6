namespace DataLayer;
public interface IDataService
{
    IList<Category> GetCategories();

    Category GetCategory(int id);
    Category CreateCategory(string name, string description);

    bool UpdateCategory(Category category);

    bool DeleteCategory(int id);

    IList<Product> GetProducts();


}
