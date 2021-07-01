using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory
{
    class ItemModel
    {
        int id;
        int price;
        string name;
        string description;

       public int getId()
        {
            return this.id;
        }
        public void setId(int id)
        {
            this.id = id;
        }

        public int getPrice()
        {
            return this.price;
        }
        public void setPrice(int price) {
            this.price = price;
        }

        public string getName()
        {
            return this.name;
        }

        public void setName(string name) {
            this.name = name;
        }

        public string getDescription()
        {
            return this.description;
        }

        public void setDescription(string name)
        {
            this.description = description;
        }
    }
}
