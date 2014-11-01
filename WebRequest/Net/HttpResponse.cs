using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Spike.Core.Net
{
    public class HttpResponse
    {
        
        private Encoding _encoding;
        public HttpResponse(Encoding encoding, WebHeaderCollection header, Stream stream, HttpStatusCode code)
        {
            _encoding = encoding;
            this.Headers = header;
            this.Stream = stream;
            this.StatusCode = code;
        }

        public HttpStatusCode StatusCode { get; private set; }

        public WebHeaderCollection Headers { get; private set; }

        public Stream Stream { get; private set; }

        private string str = "";
        public string Html
        {
            get
            {
                if (Stream != null && Stream.CanRead)
                {
                    try
                    {
                        StreamReader reader = new StreamReader(Stream, _encoding);
                        str = reader.ReadToEnd();
                        reader.Close();
                    }catch{}
                }
                return str;
            }
        }
    }
}
