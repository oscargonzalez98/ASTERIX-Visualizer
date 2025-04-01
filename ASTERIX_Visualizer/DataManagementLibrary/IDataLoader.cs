using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagementLibrary
{
    public interface IDataLoader<T>
    {
        public T loadData(string path);
    }
}
