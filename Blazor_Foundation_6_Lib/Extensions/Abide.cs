using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCodeDev.Blazor.Foundation.Extensions
{
    public static class Abide
    {
        public const string ALPHA = @"^[a-zA-Z]+$";
        public const string ALPHA_NUMERIC = @"^[a-zA-Z0-9]+$";
        public const string INTERGER = @"^[-+]?\d+$";
        public const string NUMBER = @"^[-+]?\d*(?:[\.\,]\d+)?$";

        public const string CARD = @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$";
        public const string CARD_CVV = @"^([0-9]){3,4}$";

        public const string URL = @"^((?:(https?|ftps?|file|ssh|sftp):\/\/|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}\/)(?:[^\s()<>]+|\((?:[^\s()<>]+|(?:\([^\s()<>]+\)))*\))+(?:\((?:[^\s()<>]+|(?:\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:\'"".,<>?\xab\xbb\u201c\u201d\u2018\u2019]))$";
        public const string URL_HTTP_ONLY = @"^((?:(https?):\/\/|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}\/)(?:[^\s()<>]+|\((?:[^\s()<>]+|(?:\([^\s()<>]+\)))*\))+(?:\((?:[^\s()<>]+|(?:\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:\'"".,<>?\xab\xbb\u201c\u201d\u2018\u2019]))$";

        public const string DOMAIN = @"^([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,8}$";

        public const string COLOR = @"^#?([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$";



    }
}
