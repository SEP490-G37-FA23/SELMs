using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Services.Implements
{
    public class AttachmentService : IAttachmentService
    {
        private IAttachmentRepository repository = new AttachmentRepository();

        public async Task<dynamic> SaveAttachment(HttpPostedFile file)
        {
            Attachment attachment = new Attachment()
            {
                name = file.FileName.Replace(Path.GetExtension(file.FileName), ""),
                date = DateTime.Now,
                content = new BinaryReader(file.InputStream).ReadBytes(file.ContentLength),
                extension = Path.GetExtension(file.FileName)
            };
            attachment = repository.SaveAttachment(attachment);
            return attachment;
        }
    }
}