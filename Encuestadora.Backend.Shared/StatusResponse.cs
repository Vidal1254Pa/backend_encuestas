using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encuestadora.Backend.Shared
{
    public class StatusResponse<T> : StatusSimpleResponse
    {
        public T Data { get; set; }

        public StatusResponse() : this(true, "")
        {

        }

        public StatusResponse(bool satisfactorio, string titulo) : base(satisfactorio, titulo)
        {
        }

        public StatusResponse(bool satisfactorio, string titulo, string detalle) : base(satisfactorio, titulo, detalle)
        {
        }

        public StatusResponse(string titulo, Dictionary<string, List<string>> errores) : base(titulo, errores)
        {
        }

        public StatusResponse(string titulo, string detalle, Dictionary<string, List<string>> errores) : base(titulo, detalle, errores)
        {
        }

    }
}
