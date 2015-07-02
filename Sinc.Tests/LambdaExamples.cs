using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinc.Tests
{
    class LambdaExamples
    {

        public string GetId(int v)
        {
            return (v*-1).ToString();
        }

        public string ComputeStuff(Func<int, string> f)
        {
            return f(1);
        }

        public LambdaExamples()
        {
            GetId(2);
            Func<int,string> myMethod = GetId;
            var res = ComputeStuff(myMethod);
            Func<int, string> newMethod = intParam => (intParam + 2).ToString();
            newMethod(1);
            newMethod = intParam => { return (intParam + 2).ToString(); };
            Func<int, int, string> newMethod3 = (intParam1, intParam2) => (intParam1 + intParam2).ToString();
            newMethod3(1, 2);

        }
    }
}
