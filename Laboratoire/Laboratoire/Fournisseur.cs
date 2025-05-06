using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoire
{
    class Fournisseur
    {
        private int id;
        private string nom;
        private string adresse;

        public Fournisseur(int unId, string unNom, string uneAdresse)
        {
            Id = unId;
            Nom = unNom;
            Adresse = uneAdresse;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
    }
}
