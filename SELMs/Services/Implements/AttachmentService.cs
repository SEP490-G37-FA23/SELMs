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
    public class AttachmentService : IAttachmentService
    {
        private IAttachmentRepository repository = new AttachmentRepository();

        public async Task DownloadAttachment(int id)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAttachment(int id, Attachment attachment)
        {
            if (await repository.GetAttachment(id) != null)
            {
                attachment.attach_id = id;
                repository.UpdateAttachment(attachment);
            }
        }
    }
}