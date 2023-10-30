using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Services.Implements
{
    public class ImageService : IImageService
    {
        private IImageRepository repository = new ImageRepository();
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