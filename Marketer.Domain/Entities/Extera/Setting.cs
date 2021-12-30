using Framework.Domain;
using System;

namespace Marketer.Domain.Entities.Extera
{
    public class Setting : EntityBase
    {
        public string Mobiles { get; private set; }
        public string Emails { get; private set; }
        public string Text { get; private set; }

        public Setting(string mobiles, string emails, string text)
        {
            Mobiles = mobiles;
            Emails = emails;
            Text = text;
        }

        public void Edit(string mobiles, string emails, string text)
        {
            Mobiles = mobiles;
            Emails = emails;
            Text = text;

            LastUpdateDate = DateTime.Now;
        }
    }
}
