using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents_Kylosov.Interfaces
{
    public interface IDocument
    {
        void Save(bool Update = false, bool User = false);
        List<Classes.DocumentContext> AllDocuments();
        void Delete(bool User = false);
    }
}
