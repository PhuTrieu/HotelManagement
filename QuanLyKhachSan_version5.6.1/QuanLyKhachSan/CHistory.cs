using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan
{
    [Serializable]
    class CHistory
    {
        private CBill ctmbill;

        internal CBill Ctmbill { get => ctmbill; set => ctmbill = value; }

        public CHistory(CBill ctmbill)
        {
            this.Ctmbill = ctmbill;
        }

        public CHistory() : this(new CBill())
        {
        }
    }
}
