using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Image = SELMs.Models.Image;

namespace SELMs.Services.Implements
{
    public class ImageService : IImageService
    {
        private IImageRepository repository = new ImageRepository();

        public async Task<dynamic> GetImage(int id)
        {
            Image image = await repository.GetImage(id);
            var file = System.Drawing.Image.FromStream(new MemoryStream(image.content));
            file.Save($"{image.image_name}.png", ImageFormat.Png);
            return file;
        }

        public async Task<dynamic> SaveImage(StreamContent file, string name)
        {
            Image image = new Image()
            {
                image_name = file.Headers.ContentDisposition.FileName.Replace(MimeMapping.GetMimeMapping(file.Headers.ContentDisposition.FileName),""),
                date = DateTime.Now,
                content = await file.ReadAsByteArrayAsync()
            };
            image = repository.SaveImage(image);
            return image;
        }

        public async Task UpdateImage(int id, Image image)
        {
            if (await repository.GetImage(id) != null)
            {
                image.image_id = id;
                repository.UpdateImage(image);
            }
        }
    }
}