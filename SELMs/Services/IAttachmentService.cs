using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Services
{
    public interface IAttachmentService
    {
        Task<dynamic> SaveAttachment(HttpPostedFile file);
    }
}
