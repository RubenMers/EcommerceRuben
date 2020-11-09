using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SL_WCF1
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string Saludar(string nombre);

        [OperationContract]
        Result Add(ML.Producto producto);

        [OperationContract]
        Result Update(ML.Producto producto);

        [OperationContract]
        Result Delete(ML.Producto producto);

    }

    [DataContract]
    public class Result
    {
        [DataMember]
        public Exception Ex { get; set; }
        [DataMember]
        public bool Correct { get; set; }
        [DataMember]
        public List<object> Objects { get; set; }
        [DataMember]
        public object Object { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }

    }
}
