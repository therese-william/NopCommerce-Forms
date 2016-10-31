﻿using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Forms.Models
{
    public class SubmissionListModel : BaseNopModel
    {
        public int SubmissionId { get; set; }
        public int FormId { get; set; }
        public string FormName { get; set; }
        public DateTime SubmitDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
