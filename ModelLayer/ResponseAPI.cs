using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ResponseAPI<T>
    {
        public bool Correct { get; set; }
        public T ? Valor { get; set; }
        public string ? Message { get; set; }
    }
}
