using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;

namespace ClimbingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeltoltesController : ControllerBase
    {

        public class ImageUploadModel
        {
            public string ImageData { get; set; }
        }
        [HttpPost("saveimage")]
        public async Task<IActionResult> SaveImage([FromBody] ImageUploadModel model)
        {
            try { 
            
                // A kép base64 adatának dekódolása
                var imageBytes = Convert.FromBase64String(model.ImageData.Replace("data:image/png;base64,", ""));

                // Fájlnév generálása
                var fileName = $"{Guid.NewGuid()}.png";

                // Bucket neve
                var bucketName = "rockclimbingapp";

                // Firebase Storage inicializálása
                var credential = GoogleCredential.FromFile(@"D:\_szakdolgozatFalmaszas\szakdolgozatFalmaszas\project\firebase-adminsdk.json");
                var storage = StorageClient.Create(credential);

                using (var stream = new MemoryStream(imageBytes))
                {
                    await storage.UploadObjectAsync(bucketName, fileName, "image/png", stream);
                }

                var publicUrl = $"https://firebasestorage.googleapis.com/v0/b/{bucketName}/o/{Uri.EscapeDataString(fileName)}?alt=media";

                return Ok(new { url = publicUrl });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba:{ex.Message}");
            }
        }
    }
}

