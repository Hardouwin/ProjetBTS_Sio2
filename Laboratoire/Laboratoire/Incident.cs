using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoire
{
    class Incident
    {
        private int id;
        private string descriptionincident;
        private DateTime dateDeclarationIncident;
        private string urgenceIncident;
        private string statutIncident;
        private string travailEffectue;
        private DateTime? dateTraitementIncident;
        private int? tempsResolutionIncident;
        private string posteIncident;
        private int idMateriel;
        private int idUtilisateur;

        public Incident(int unId, string unedescriptionincident, DateTime uneDateDeclarationIncident, string uneUrgenceIncident, string unStatutIncident, string unTravailEffectue, DateTime? uneDdateTraitementIncident, int? unTempsResolutionIncident, string unPosteIncident, int unIdMateriel, int unIdUtilisateur)
        {
            this.Id = unId;
            this.Descriptionincident = unedescriptionincident;
            this.DateDeclarationIncident = uneDateDeclarationIncident;
            this.UrgenceIncident = uneUrgenceIncident;
            this.StatutIncident = unStatutIncident;
            this.TravailEffectue = unTravailEffectue;
            this.DateTraitementIncident = uneDdateTraitementIncident;
            this.TempsResolutionIncident = unTempsResolutionIncident;
            this.PosteIncident = unPosteIncident;
            this.idMateriel = unIdMateriel;
            this.idUtilisateur = unIdUtilisateur;
        }

        public int Id { get => id; set => id = value; }
        public string Descriptionincident { get => descriptionincident; set => descriptionincident = value; }
        public DateTime DateDeclarationIncident { get => dateDeclarationIncident; set => dateDeclarationIncident = value; }
        public string UrgenceIncident { get => urgenceIncident; set => urgenceIncident = value; }
        public string StatutIncident { get => statutIncident; set => statutIncident = value; }
        public string TravailEffectue { get => travailEffectue; set => travailEffectue = value; }
        public DateTime? DateTraitementIncident { get => dateTraitementIncident; set => dateTraitementIncident = value; }
        public int? TempsResolutionIncident { get => tempsResolutionIncident; set => tempsResolutionIncident = value; }
        public int IdMateriel { get => idMateriel; set => idMateriel = value; }
        public int IdUtilisateur { get => idUtilisateur; set => idUtilisateur = value; }
        internal string PosteIncident { get => posteIncident; set => posteIncident = value; }
    }
}
