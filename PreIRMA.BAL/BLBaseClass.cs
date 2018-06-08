using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreIRMA.BAL
{
    public abstract class BLBaseClass
    {
        public int GetSuccessCode
        {
            get { return 1; }
        }
        public int GetErrorCode
        {
            get { return -1; }
        }
    }
}
