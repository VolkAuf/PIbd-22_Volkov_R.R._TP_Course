using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedBusinessLogic.HelperModels.Mail
{
    public class MailSendInfo
    {
        public string MailAddress { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string FileName { get; set; }
    }
}
