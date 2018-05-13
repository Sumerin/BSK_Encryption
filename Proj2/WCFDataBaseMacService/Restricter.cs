using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFDataBaseMacService
{
    public static class Restricter<Src, Dst>
    {
        public static List<Dst> Restrict(IEnumerable<Src> input, Func<Src, Dst> restrictFunction)
        {
            var result = new List<Dst>();
            foreach (var item in input)
            {
                var restrictResult = restrictFunction(item);
                if(restrictResult!=null)
                {
                    result.Add(restrictResult);
                }
            }
            return result;
        }
    }
}