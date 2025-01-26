﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳.Attributes
{
    internal class ImageColumnAttribute : Attribute
    {
        public int Index = 0;
        public ImageColumnAttribute(int index)
        {
            this.Index = index;
        }
    }
}
