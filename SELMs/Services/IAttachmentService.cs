using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Services
{
    public interface IAttachmentService
    {
        Task DownloadAttachment(int id);
        Task UpdateAttachment(int id, Attachment attachment);
    }
}
