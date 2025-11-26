namespace ClimbingApplication.Service
{
    public interface IImageService
    {
        // Fájlnév kinyerése
        string ExtractFileNameFromUrl(string url);

        // Kép törlése a Firebase Storage-ból
        Task DeleteImageAsync(string imageUrl);
    }
}
