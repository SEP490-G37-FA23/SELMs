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
        void SaveImage(Image image);
        void SaveImages(List<Image> images);
        void UpdateImage(Image image);
        void DeleteImage(int id);
    }
}
