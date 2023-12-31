﻿using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Services
{
    public interface IImageService
    {
        System.Drawing.Image GetImage(int id);
        Task<dynamic> SaveImage(HttpPostedFile file, string name);
        Task UpdateImage(int id, Image image);
    }
}
