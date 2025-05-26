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
        private static readonly string bucketNev = "climbingapplication.appspot.com";
        private static bool firebaseInistalized = false;

        public FeltoltesController()
        {
            if (!firebaseInistalized)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "secrets", "firebase-adminsdk.json");
                Console.WriteLine("Firebase path: " + path);

                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(path),
                    ProjectId = "iotcloud2025"
                });
                firebaseInistalized = true;
            }
        }
        public class ImageDataRequest
        {
            public string ImageData { get; set; }
        }

            [HttpPost]
            public async Task<IActionResult> POST([FromBody] ImageDataRequest request)
            {
            try
            {

                if (string.IsNullOrEmpty(request.ImageData))
                {
                    return BadRequest("Nincs képadat.");
                }
                var base64 = request.ImageData.Split(',')[1];
                var imageBytes = Convert.FromBase64String(base64);

                var imageName = $"climbingapplication/images/{Guid.NewGuid()}.png";

                var storage = StorageClient.Create();
                using (var stream = new MemoryStream(imageBytes))
                {
                    await storage.UploadObjectAsync(bucketNev, imageName, "image/png", stream);
                }

                var publicUrl = $"https://storage.googleapis.com/{bucketNev}/{imageName}";
                return Ok(new { url = publicUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba: {ex.Message}");
            }
            }
        }
    }

