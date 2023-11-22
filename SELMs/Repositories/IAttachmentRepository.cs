using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Repositories
{
    public interface IAttachmentRepository
    {
        dynamic GetAttachment(int id);
        dynamic SaveAttachment(Attachment attachment);
        dynamic SaveAttachments(List<Attachment> attachments);
        dynamic UpdateAttachment(Attachment attachment);
        void DeleteAttachment(int id);
    }
}
