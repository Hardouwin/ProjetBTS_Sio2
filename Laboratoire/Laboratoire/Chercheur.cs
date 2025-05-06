using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoire
{
    class Chercheur
    {
        private int id;
        private string nom;
        private string prenom;
        private string specialite;
        private string annee;

        public Chercheur(int unId, string unNom, string unPrenom, string unSpecialite, string unAnnee)
        {
            this.id = unId;
            this.nom = unNom;
            this.prenom = unPrenom;
            this.specialite = unSpecialite;
            this.annee = unSpecialite;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Specialite { get => specialite; set => specialite = value; }
        public string Annee { get => annee; set => annee = value; }
    }
}

