﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDebtonator.Helpers
{
    public interface IPageViewModel
    {
        string Name { get; }
        string Title { get; set; }
    }
}
