﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLibrary
{
    public interface IMessage
    {
        ParsedMessage parseData(IMessage message);
    }
}
