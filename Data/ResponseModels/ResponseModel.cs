using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ResponseModels
{
    public class ResponseModel<T>
    {
        public string Model { get; set; }

        public T Data { get; set; }

        public string[] Messages { get; set; }

        public ResponseModel(string model, string[] messages, T data)
        {
            this.Model = model;
            this.Messages = messages;
            this.Data = data;
        }
    }
}
