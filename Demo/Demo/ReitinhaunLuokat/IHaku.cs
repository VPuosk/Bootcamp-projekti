using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ReitinhaunLuokat
{
    public interface IHaku
    {
        public void Suorita();
        public List<string> Tulosta();
    }
}
