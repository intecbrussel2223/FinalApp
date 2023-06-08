namespace FinalApp.Models
{
    public class ImageHelper : IImageHelper
    {
        private readonly string _imageFolderPath;

        public ImageHelper(IWebHostEnvironment hostingEnvironment)
        {
            _imageFolderPath = Path.Combine(hostingEnvironment.WebRootPath, "Images");
        }
        public string GetImageUrl(string fileName)
        {
            string filePath = Path.Combine(_imageFolderPath, fileName);
            if (File.Exists(filePath))
            {
                string imageUrl = "/images/" + fileName; 
                return imageUrl;
            }
            return "/images/Default.jpg";
        }

        public string StoreImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return string.Empty;
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            //string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file+".jpg");
            string filePath = Path.Combine(_imageFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

    }
}
