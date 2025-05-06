using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoire
{
    class Materiel
    {
        private int id;
        private string caracteristique;
        private DateTime? dateAchat;
        private DateTime? dateLoc;
        private DateTime? garantie;
        private int idUtilisateur;
        private int idFournisseur;

        public Materiel(int unId, string uneCaracteristique, DateTime? uneDateAchat, DateTime? uneDateLoc, DateTime? uneGarantie, int idUtilisateur, int idFournisseur)
        {
            this.Id = unId;
            this.Caracteristique = uneCaracteristique;
            this.DateAchat = uneDateAchat;
            this.DateLoc = uneDateLoc;
            this.Garantie = uneGarantie;
            this.IdUtilisateur = idUtilisateur;
            this.IdFournisseur = idFournisseur;
        }

        public int Id { get => id; set => id = value; }
        public string Caracteristique { get => caracteristique; set => caracteristique = value; }
        public DateTime? DateAchat { get => dateAchat; set => dateAchat = value; }
        public DateTime? DateLoc { get => dateLoc; set => dateLoc = value; }
        public DateTime? Garantie { get => garantie; set => garantie = value; }
        public int IdUtilisateur { get => idUtilisateur; set => idUtilisateur = value; }
        public int IdFournisseur { get => idFournisseur; set => idFournisseur = value; }
    }
}
