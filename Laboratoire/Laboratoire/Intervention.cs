using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratoire
{
    class Intervention
    {
        private int id;
        private string descPEC;
        private string typeIntervention;
        private int idTechnicien;
        private int idIncident;

        public Intervention(int unId, string descPEC, string unTypeIntervention, int unIdTechnicien,  int unIdIncident)
        {
            this.id = unId;
            this.descPEC = descPEC;
            this.typeIntervention = unTypeIntervention;
            this.idTechnicien = unIdTechnicien;
            this.idIncident = unIdIncident;
        }

        public int Id { get => id; set => id = value; }
        public string DescPEC { get => descPEC; set => descPEC = value; }
        public string TypeIntervention { get => typeIntervention; set => typeIntervention = value; }
        public int IdTechnicien { get => idTechnicien; set => idTechnicien = value; }
        public int IdIncident { get => idIncident; set => idIncident = value; }
    }
}
