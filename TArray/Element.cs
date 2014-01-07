using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TArray
{
    class Element
    {
        private Object data;
        public Object Data
        {
            get { return data; }
            set { data = value; }
        }

        private Element next;
        public Element Next
        {
            get { return next; }
            set { next = value; }
        }

        private Element previous;
        public Element Previous
        {
            get { return previous; }
            set { previous = value; }
        }

        public Element(Object data)
        {
            this.data = data;
            next = null;
            previous = null;
        }

    }
}
