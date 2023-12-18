using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Repositories.Implements
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private SELMsContext db = new SELMsContext();
        public void DeleteAttachment(int id)
        {
            dynamic attachment = db.Attachments.Where(i => i.attach_id == id).FirstOrDefault();
            if (attachment != null) db.Attachments.Remove(attachment);
            db.SaveChangesAsync();
        }

        public dynamic GetAttachment(int id)
        {
            dynamic attachment = db.Attachments.Where(e => e.attach_id == id).FirstOrDefault();
            return attachment;
        }

        public dynamic SaveAttachment(Attachment attachment)
        {
            db.Attachments.Add(attachment);
            db.SaveChanges();
            return attachment;
        }

        public dynamic SaveAttachments(List<Attachment> attachments)
        {
            db.Attachments.AddRange(attachments);
            db.SaveChangesAsync();
            return attachments;
        }

        public dynamic UpdateAttachment(Attachment attachment)
        {
            Attachment orgAttachment = db.Attachments.Where(e => e.attach_id == attachment.attach_id).FirstOrDefault();
            db.Entry(orgAttachment).CurrentValues.SetValues(attachment);
            db.SaveChangesAsync();
            return orgAttachment;
        }
    }
}