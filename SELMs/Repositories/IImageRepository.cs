using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Repositories
{
    public interface IImageRepository
    {
        dynamic GetImage(int id);
        dynamic SaveImage(Image image);
        dynamic SaveImages(List<Image> images);
        void UpdateImage(Image image);
        void DeleteImage(int id);
    }
}
