﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueFalseInterface
{
    public class Question
    {
        public string Text { get; set; }
        public bool TrueFalse { get; set; }
        public Question(string text, bool trueFalse)
        {
            Text = text;
            TrueFalse = trueFalse;
        }
        public Question()
        {
            Text = "Земля круглая?";
            TrueFalse = true;
        }
    }
}
