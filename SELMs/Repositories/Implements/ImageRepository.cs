using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Repositories.Implements
{
    public class ImageRepository : IImageRepository
    {
        private SELMsContext db = new SELMsContext();
        public void DeleteImage(int id)
        {
            dynamic image = db.Images.Where(i => i.image_id == id).FirstOrDefault();
            if (image != null) db.Categories.Remove(image);
            db.SaveChangesAsync();
        }

        public dynamic GetImage(int id)
        {
            dynamic image = db.Images.Where(e => e.image_id == id).FirstOrDefault();
            return image;
        }

        public void SaveImage(Image image)
        {
            db.Images.Add(image);
            db.SaveChangesAsync();
        }

        public void SaveImages(List<Image> images)
        {
            db.Images.AddRange(images);
            db.SaveChangesAsync();
        }

        public void UpdateImage(Image image)
        {
            Image orgImage = db.Images.Where(e => e.image_id == image.image_id).FirstOrDefault();
            db.Entry(orgImage).CurrentValues.SetValues(image);
            db.SaveChangesAsync();
        }
    }
}