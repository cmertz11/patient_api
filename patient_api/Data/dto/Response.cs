using Org.BouncyCastle.Crypto.Prng.Drbg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patient_api.Data.dto
{
    public class Response<T>
    {
        public Response() { }

        public Response(T response)
        {
            Data = response;
        }
        public T Data { get; set; }

    }
}
