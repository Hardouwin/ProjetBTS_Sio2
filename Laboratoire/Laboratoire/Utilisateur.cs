using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoire
{
    class Utilisateur
    {
        private int id;
        private string nom;
        private string prenom;
        private string mdp;

        public Utilisateur(int unId,  string unNom, string unPrenom, string unMdp)
        {
            this.Id = unId;
            this.Nom = unNom;
            this.Prenom = unPrenom;
            this.Mdp = unMdp;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Mdp { get => mdp; set => mdp = value; }
    }
}
