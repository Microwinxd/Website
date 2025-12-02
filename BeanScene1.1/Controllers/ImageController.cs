using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    [HttpGet("generate")]
    public IActionResult Generate([FromQuery] string? text = "Hello MVC + Blazor!")
    {
        using var bmp = new Bitmap(400, 200);
        using var g = Graphics.FromImage(bmp);

        g.Clear(Color.Beige); 
        using var font = new Font("Arial", 18, FontStyle.Bold); 
        g.DrawString(text, font, Brushes.DarkSlateBlue, new PointF(20, 80));

        using var ms = new MemoryStream(); 
        bmp.Save(ms, ImageFormat.Png); 
        return File(ms.ToArray(), "image/png");
    }
}