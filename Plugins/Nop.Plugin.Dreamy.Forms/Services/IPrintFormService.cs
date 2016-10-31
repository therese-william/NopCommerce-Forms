using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Forms.Services
{
    public interface IPrintFormService
    {
        string CreateFormPdf(int SubmissionId);
    }
}
