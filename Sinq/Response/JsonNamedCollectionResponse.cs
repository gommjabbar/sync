using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Sinq.Response
{
    public class JsonCollectionResponse<T> : GenericResponse<IEnumerable<T>>
    {
        /// <summary>
        /// Named result
        /// </summary>
        public IEnumerable<T> Result { get { return _responseObject as IEnumerable<T>; } }

        public JsonCollectionResponse(HttpRequestMessage request, Func<IEnumerable<T>> getResponseCollection, string collectionName = null)
            : base(request, getResponseCollection)
        {
        }
    }
}