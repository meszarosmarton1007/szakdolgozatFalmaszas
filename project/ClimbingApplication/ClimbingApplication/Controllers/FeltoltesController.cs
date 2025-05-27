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
        private readonly string bucketName = "climbingapplication.appspot.com";
        private readonly string ProjectId = "iotcloud2025";

        public class ImageUploadModel
        {
            public string ImageData { get; set; }
        }
        [HttpPost("saveimage")]
        public async Task<IActionResult> SaveImage([FromBody] ImageUploadModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.ImageData))
                {
                    return BadRequest("Hiányzik a kép");
                }

                var base64Data = model.ImageData.Contains(",") ? model.ImageData.Split(",")[1] : model.ImageData;

                byte[] imageBytes = Convert.FromBase64String(base64Data);

                string fileName = $"images/{Guid.NewGuid()}.png";

                var storage = StorageClient.Create();

                using var stream = new MemoryStream(imageBytes);
                await storage.UploadObjectAsync(bucketName, fileName, "image/png", stream, new UploadObjectOptions
                {
                    PredefinedAcl = PredefinedObjectAcl.PublicRead
                });

                string publicUrl = $"https://storage.googleapis.com/{bucketName}/{fileName}";
                return Ok(new { imageUrl = publicUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba:{ex.Message}");
            }
        }
    }
}

