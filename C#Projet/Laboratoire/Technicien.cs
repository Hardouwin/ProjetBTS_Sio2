using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoire
{
    class Technicien
    {
        private int id;
        private string nom;
        private string prenom;
        private string matricule;
        private DateTime dateEmbauche;
        private string niveau;
        private List<Competences> competences;
        private string formation;
        private string mdp;
        private int idResponsable;

        public Technicien(int unId, string unNom, string unPrenom, string uneMatricule, DateTime uneDateEmbauche, string unNiveau, List<Competences> desCompetences, string uneFormation, string unMdp, int unIdResponsable)
        {
            this.id = unId;
            this.nom = unNom;
            this.prenom = unPrenom;
            this.matricule = uneMatricule;
            this.dateEmbauche = uneDateEmbauche;
            this.niveau = unNiveau;
            this.competences = desCompetences;
            this.formation = uneFormation;
            this.mdp = unMdp;
            this.IdResponsable = unIdResponsable;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Matricule { get => matricule; set => matricule = value; }
        public DateTime DateEmbauche { get => dateEmbauche; set=> dateEmbauche = value; }
        public string Niveau { get => niveau; set => niveau = value; }
        public List<Competences> Competences { get => competences; set => competences = value; }
        public string Formation { get => formation ; set => formation = value; }
        public string Mdp { get => mdp; set => mdp = value; }
        public int IdResponsable { get => idResponsable; set => idResponsable = value; }
    }
}
