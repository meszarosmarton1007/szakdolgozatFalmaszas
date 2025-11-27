using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace ClimbingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeltoltesController : ControllerBase
    {
        private readonly string _bucket;
        private readonly string _credentialPath;

        public FeltoltesController(IConfiguration configuration)
        {
            _bucket = configuration["Firebase:StorageBucket"];
            _credentialPath = configuration["Firebase:ServiceAccountPath"];
        }

        public class ImageUploadModel
        {
            public string ImageData { get; set; }
        }

        //Kép feltöltése
        [HttpPost("saveimage")]
        public async Task<IActionResult> SaveImage([FromBody] ImageUploadModel model)
        {
            try { 
            
                // A kép base64 adatának dekódolása
                var imageBytes = Convert.FromBase64String(model.ImageData.Replace("data:image/png;base64,", ""));

                // Fájlnév generálása
                var fileName = $"{Guid.NewGuid()}.png";

                var storage = StorageClient.Create(GoogleCredential.FromFile(_credentialPath));

                using var stream = new MemoryStream(imageBytes);
                
                await storage.UploadObjectAsync(bucket: _bucket, objectName: fileName, contentType: "image/png", source: stream);
                
                var publicUrl = $"https://firebasestorage.googleapis.com/v0/b/{_bucket}/o/{Uri.EscapeDataString(fileName)}?alt=media";

                return Ok(new { url = publicUrl });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba:{ex.Message}");
            }
        }

        //Kép törlése
        [HttpDelete("deleteimage")]
        public async Task<IActionResult> DeleteImage([FromQuery] string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("Hiányzik a file");
            }

            try
            {
                var storage = StorageClient.Create(GoogleCredential.FromFile(_credentialPath));

                await storage.DeleteObjectAsync(_bucket, fileName);

                return Ok("A kép sikeresen törölve");

            }
            catch (Google.GoogleApiException ex)
            {
                if (ex.Error?.Code == 404)
                {
                    return NotFound("A kép nem található");
                }

                return StatusCode(500, $"Hiba történt a törlés során: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Váratlan hiba történt: {ex.Message}");
            }

        }

        public string ExtracktFileNameFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            var uri = new Uri(url);
            var path = uri.LocalPath;
            var fileName = Uri.UnescapeDataString(path.Split('/').Last());

            return fileName;
        }


    }
}

