namespace CaseStudyAdminMVC.ServicesAbstract
{
    public interface IProductService
    {
        Task<bool> DeleteProductAsync(int id);

    }
}
