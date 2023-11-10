using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Models.BusinessModel
{
    public static class ApplicationStatus
    {
        public const string DRAFT = "DRAFT";
        public const string PENDING = "PENDING";
        public const string APPROVED = "APPROVED";
        public const string REJECTED = "REJECTED";
        public const string CANCELED = "CANCELED";
    }
}