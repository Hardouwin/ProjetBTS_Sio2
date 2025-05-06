using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoire
{
    internal class Competences
    {
        private int id;
        private string description;

        public Competences(int unId, string uneDescription)
        {
            this.Id = unId;
            this.Description = uneDescription;
        }

        public int Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
    }
}
