using System;
using System.Collections.Generic;
using System.Text;

namespace JobSearch.App.Models
{
    public class ResponseService<T>
    {
        // boleano que define se o request ocorreu com sucesso
        public bool IsSucess { get; set; }
        // código de status da response (estados)
        public int StatusCode { get; set; }
        public T Data { get; set; }
        //Mensagens definidas para diferentes erros
        public Dictionary<string, List<string>> Errors  { get; set; }

    }
}
