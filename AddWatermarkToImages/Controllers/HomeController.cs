using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddWatermarkToImages.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string text,HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string value = text;
                string file = Path.GetFileNameWithoutExtension(postedFile.FileName) + ".png";
                using (Bitmap bitmap = new Bitmap(postedFile.InputStream, false))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        Brush brush = new SolidBrush(Color.Red);
                        Font font = new Font("Arial", 90, FontStyle.Italic, GraphicsUnit.Pixel);
                        SizeF textSize = new SizeF();
                        textSize = graphics.MeasureString(value, font);
                        Point position = new Point(bitmap.Width - ((int)textSize.Width + 10), bitmap.Height - ((int)textSize.Height + 10));
                        graphics.DrawString(value, font, brush, position);
                        using (MemoryStream mStream = new MemoryStream())
                        {
                            bitmap.Save(mStream, ImageFormat.Png);
                            mStream.Position = 0;
                            return File(mStream.ToArray(), "image/png", file);
                        }
                    }
                }
            }
            return View();
        }

    }
}