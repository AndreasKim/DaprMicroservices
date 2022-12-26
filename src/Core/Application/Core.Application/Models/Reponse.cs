using Core.Application.Interfaces;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Models
{
    public class Reponse<T> : IResponse<T>
    {
        public T? Value { get; set; }    
    }
}
