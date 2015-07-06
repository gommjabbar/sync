﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Sinq.Response
{
    /// <summary>
    /// A HttpActionResult implementation that builds a HttpResponseMessage that has as serialized json as its Content
    /// </summary>
    /// <typeparam name="T">The type of the SingleObjectResponse Result property</typeparam>
    public class JsonResponse<T> : GenericResponse<T>
    {
        /// <summary>
        /// Named result
        /// </summary>
        public T Result { get { return (T)_responseObject; } }

        public JsonResponse(HttpRequestMessage request, Func<T> getResponseValueMethod)
            : base(request)
        {
            _getResponseMethod = () => getResponseValueMethod();
        }
    }
}