namespace FinalApp.Models
{
    public interface IImageHelper
    {
        string GetImageUrl(string fileName);
        string StoreImage(IFormFile file);
    }
}
