using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Laboratoire
{
    internal class Bd
    {
        private static string connstring = "Server=127.0.0.1;Database=projetlaboratoire;Uid=root;Password=;";
        private static MySqlConnection connec = new MySqlConnection(connstring);
        private static List<Materiel> lesMateriels;
        private static List<Incident> lesIncidents;
        private static List<Utilisateur> lesUtilisateurs;
        private static List<Fournisseur> lesFournisseurs;
        private static List<Chercheur> lesChercheur;
        private static List<Competences> lesCompetences;
        private static List<Technicien> lesTechniciens;
        private static List<Incident> lesIncidentsUtilisateur;
        private static string competence;
        private static string competencebis;
        private static string listIdUtilisateur;
        private static string listIdTechnicien;

        public Bd() 
        {
        }

        /// <summary>
        /// Vérifie les informations de connexion pour trois types d’utilisateurs (utilisateur, technicien, et responsable). 
        /// </summary>
        /// <param name="user">L'id de l'utilisateur</param>
        /// <param name="mdp">Le mot de passe de l'utilisateur</param>
        /// <returns>Selon le type d’utilisateur trouvé, renvoie une valeur (1, 2, ou 3) pour indiquer le type d’utilisateur, et 0 si les informations ne correspondent à aucun utilisateur.</returns>
        public static int connexion(string user, string mdp) 
        {
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            MySqlCommand cmd2 = connec.CreateCommand();
            MySqlCommand cmd3 = connec.CreateCommand();
            cmd.CommandText = "SELECT * FROM utilisateur WHERE idUtilisateur = @id AND mdpUtilisateur = @mdp";
            cmd.Parameters.AddWithValue("@id", user);
            cmd.Parameters.AddWithValue("@mdp", mdp);
            cmd2.CommandText = "SELECT * FROM technicien WHERE idTechnicien = @id AND mdpTechnicien = @mdp";
            cmd2.Parameters.AddWithValue("@id", user);
            cmd2.Parameters.AddWithValue("@mdp", mdp);
            cmd3.CommandText = "SELECT * FROM responsable WHERE idResponsable = @id AND mdpResponsable = @mdp";
            cmd3.Parameters.AddWithValue("@id", user);
            cmd3.Parameters.AddWithValue("@mdp", mdp);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                connec.Close();
                return 1;
            }
            else 
            {
                reader.Close();
                MySqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.HasRows)
                {
                    connec.Close();
                    return 2;
                }
                else
                {
                    reader2.Close();
                    MySqlDataReader reader3 = cmd3.ExecuteReader();
                    if (reader3.HasRows)
                    {
                        connec.Close();
                        return 3;
                    }
                    else 
                    {
                        connec.Close();
                        reader3.Close();
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Ajoute un nouvel enregistrement de matériel dans la base de données en insérant les informations fournies dans la table materiel.
        /// </summary>
        /// <param name="unMateriel">L'objet Materiel contenant les informations du matériel à insérer.</param>

        public static void ajoutMateriel(Materiel unMateriel) 
        {
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "INSERT INTO materiel(idMateriel, caracteristiquesMateriel, dateAchat, dateLocation, garantieMateriel, idUtilisateur, idFournisseur) values (@id, @caract, @dateAchat, @dateLocation, @garantieMateriel, @idUtilisateur, @idFournisseur)";
            cmd.Parameters.AddWithValue("@id", unMateriel.Id);
            cmd.Parameters.AddWithValue("@caract", unMateriel.Caracteristique);
            cmd.Parameters.AddWithValue("@dateAchat", unMateriel.DateAchat);
            cmd.Parameters.AddWithValue("@dateLocation", unMateriel.DateLoc);
            cmd.Parameters.AddWithValue("@garantieMateriel", unMateriel.Garantie);
            cmd.Parameters.AddWithValue("@idUtilisateur", unMateriel.IdUtilisateur);
            cmd.Parameters.AddWithValue("@idFournisseur", unMateriel.IdFournisseur);
            cmd.ExecuteNonQuery();
            connec.Close();
        }

        /// <summary>
        /// Supprime un enregistrement de matériel de la base de données en utilisant l'identifiant du matériel.
        /// </summary>
        /// <param name="unMateriel">L'objet Materiel contenant l'identifiant du matériel à supprimer.</param>
        public static void supprimerMateriel(Materiel unMateriel)
        {
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "DELETE FROM materiel WHERE idMateriel = @id";
            cmd.Parameters.AddWithValue("@id", unMateriel.Id);
            cmd.ExecuteNonQuery();
            connec.Close();
        }


        /// <summary>
        /// Récupère tous les matériels de la base de données en lisant chaque enregistrement de la table materiel, puis les stocke dans une liste pour retour.
        /// </summary>
        /// <returns>Une liste d'objets Materiel contenant tous les enregistrements de matériel de la base de données.</returns>
        public static List<Materiel> GetMateriels()
        {
            lesMateriels = new List<Materiel>();
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "SELECT * FROM materiel";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idMateriel = reader.GetInt32("idMateriel");
                string caracteristiquesMateriel = reader.GetString("caracteristiquesMateriel");
                DateTime dateAchat = reader.GetDateTime("dateAchat");

                // Vérification pour éviter les erreurs si des champs peuvent être NULL
                DateTime? dateLocation = reader.IsDBNull(reader.GetOrdinal("dateLocation"))
                    ? (DateTime?)null
                    : reader.GetDateTime("dateLocation");

                DateTime? garantieMateriel = reader.IsDBNull(reader.GetOrdinal("garantieMateriel"))
                    ? (DateTime?)null
                    : reader.GetDateTime("garantieMateriel");

                int idUtilisateur = reader.GetInt32("idUtilisateur");
                int idFournisseur = reader.GetInt32("idFournisseur");

                Materiel unMateriel = new Materiel(
                    idMateriel,
                    caracteristiquesMateriel,
                    dateAchat,
                    dateLocation,
                    garantieMateriel,
                    idUtilisateur,
                    idFournisseur
                );

                lesMateriels.Add(unMateriel);
            }
            connec.Close();
            return lesMateriels;
        }


        /// <summary>
        /// Ajoute un nouvel enregistrement d’incident dans la base de données avec les informations fournies, en incluant des champs pouvant être laissés NULL.
        /// </summary>
        /// <param name="unIncident">L'objet Incident contenant les informations de l'incident à insérer.</param>
        public static void ajoutIncident(Incident unIncident)
        {
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();

            cmd.CommandText = "INSERT INTO incident(idIncident, descriptionIncident, dateDeclarationIncident, urgenceIncident, statutIncident, travailEffectue, dateTraitementIncident, tempsResolutionIncident, posteIncident, idMateriel, idUtilisateur) VALUES (@id, @description, @dateDeclaration, @urgence, @statut, @travail, @dateTraitement, @tempsResolution, @poste, @idMateriel, @idUtilisateur)";

            cmd.Parameters.AddWithValue("@id", unIncident.Id);
            cmd.Parameters.AddWithValue("@description", unIncident.Descriptionincident);
            cmd.Parameters.AddWithValue("@dateDeclaration", unIncident.DateDeclarationIncident);
            cmd.Parameters.AddWithValue("@urgence", unIncident.UrgenceIncident);
            cmd.Parameters.AddWithValue("@statut", unIncident.StatutIncident);
            cmd.Parameters.AddWithValue("@travail", DBNull.Value);
            cmd.Parameters.AddWithValue("@dateTraitement", DBNull.Value);
            cmd.Parameters.AddWithValue("@tempsResolution", DBNull.Value);
            cmd.Parameters.AddWithValue("@poste", unIncident.PosteIncident);
            cmd.Parameters.AddWithValue("@idMateriel", unIncident.IdMateriel);
            cmd.Parameters.AddWithValue("@idUtilisateur", unIncident.IdUtilisateur);

            cmd.ExecuteNonQuery();
            connec.Close();
        }


        /// <summary>
        /// Récupère tous les incidents de la base de données, lit chaque enregistrement de la table incident et les stocke dans une liste pour les retourner.
        /// </summary>
        /// <returns>Une liste d'objets Incident contenant tous les enregistrements d'incidents de la base de données.</returns>
        public static List<Incident> GetIncidents()
        {
            lesIncidents = new List<Incident>();
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "SELECT * FROM incident";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idIncident = reader.GetInt32("idIncident");
                string descriptionIncident = reader.GetString("descriptionIncident");
                DateTime dateDeclarationIncident = reader.GetDateTime("dateDeclarationIncident");
                string urgenceIncident = reader.GetString("urgenceIncident");
                string statutIncident = reader.GetString("statutIncident");

                // Champs pouvant être NULL
                string travailEffectue = reader.IsDBNull(reader.GetOrdinal("travailEffectue"))
                    ? string.Empty
                    : reader.GetString("travailEffectue");

                DateTime? dateTraitementIncident = reader.IsDBNull(reader.GetOrdinal("dateTraitementIncident"))
                    ? (DateTime?)null
                    : reader.GetDateTime("dateTraitementIncident");

                int? tempsResolutionIncident = reader.IsDBNull(reader.GetOrdinal("tempsResolutionIncident"))
                    ? (int?)null
                    : reader.GetInt32("tempsResolutionIncident");

                // Champs non NULL
                string posteIncident = reader.GetString("posteIncident");
                int idMateriel = reader.GetInt32("idMateriel");
                int idUtilisateur = reader.GetInt32("idUtilisateur");

                // Crée un nouvel objet Incident avec les valeurs récupérées
                Incident unIncident = new Incident(
                    idIncident,
                    descriptionIncident,
                    dateDeclarationIncident,
                    urgenceIncident,
                    statutIncident,
                    travailEffectue,
                    dateTraitementIncident,
                    tempsResolutionIncident,
                    posteIncident,
                    idMateriel,
                    idUtilisateur
                );

                lesIncidents.Add(unIncident);
            }
            connec.Close();
            return lesIncidents;
        }


        /// <summary>
        /// Ajoute un enregistrement d'intervention lié à un incident, en enregistrant la prise en charge dans la table intervention.
        /// </summary>
        /// <param name="uneIntervention">L'objet Intervention contenant les informations de l'intervention à enregistrer.</param>
        public static void priseEnChargeIncident(Intervention uneIntervention)
        {
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "INSERT INTO intervention(idIntervention, descPec, typeIntervention, idTechnicien, idIncident) values (@idIntervention, @desc, @type, @idTechnicien, @idIncident)";
            cmd.Parameters.AddWithValue("@idIntervention", uneIntervention.Id);
            cmd.Parameters.AddWithValue("@desc", uneIntervention.DescPEC);
            cmd.Parameters.AddWithValue("@type", uneIntervention.TypeIntervention);
            cmd.Parameters.AddWithValue("@idTechnicien", uneIntervention.IdTechnicien);
            cmd.Parameters.AddWithValue("@idIncident", uneIntervention.IdIncident);
            cmd.ExecuteNonQuery();
            connec.Close();
        }


        /// <summary>
        /// Modifie les informations d'un incident dans la base de données en mettant à jour le statut et le travail effectué.
        /// </summary>
        /// <param name="unIncident">L'objet Incident contenant les informations de l'incident à mettre à jour, y compris le nouvel état et le travail effectué.</param>
        public static void modifIncident(Incident unIncident)
        {
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "UPDATE incident SET statutIncident = @statut, travailEffectue = @travaileffectue WHERE idIncident = @id";
            cmd.Parameters.AddWithValue("@statut", unIncident.StatutIncident);
            cmd.Parameters.AddWithValue("@travaileffectue", unIncident.TravailEffectue);
            cmd.Parameters.AddWithValue("@id", unIncident.Id);
            cmd.ExecuteNonQuery();
            connec.Close();
        }


        /// <summary>
        /// Ajoute un technicien à la base de données en insérant ses informations et ses compétences.
        /// </summary>
        /// <param name="unTechnicien">L'objet Technicien contenant les informations du technicien à insérer, y compris ses compétences.</param>
        public static void ajoutTechnicien(Technicien unTechnicien)
        {
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            foreach (Competences competences in unTechnicien.Competences)
            {
                competence += competences.Id + ";";
            }
            cmd.CommandText = "INSERT INTO technicien(idTechnicien, nomTechnicien, prenomTechnicien, matriculeTechnicien, dateEmbPersonnel, niveauTechnicien, competencesTechnicien, formationTechnicien, mdpTechnicien, idResponsable) values (@idTechnicien, @nomTechnicien, @prenomTechnicien,  @matriculeTechnicien, @dateEmbPersonnel, @niveauTechnicien, @competencesTechnicien, @formationTechnicien, @mdpTechnicien, @idResponsable)";
            cmd.Parameters.AddWithValue("@idTechnicien", unTechnicien.Id);
            cmd.Parameters.AddWithValue("@nomTechnicien", unTechnicien.Nom);
            cmd.Parameters.AddWithValue("@prenomTechnicien", unTechnicien.Prenom);
            cmd.Parameters.AddWithValue("@matriculeTechnicien", unTechnicien.Matricule);
            cmd.Parameters.AddWithValue("@dateEmbPersonnel", unTechnicien.DateEmbauche);
            cmd.Parameters.AddWithValue("@niveauTechnicien", unTechnicien.Niveau);
            cmd.Parameters.AddWithValue("@competencesTechnicien", competence);
            cmd.Parameters.AddWithValue("@formationTechnicien", unTechnicien.Formation);
            cmd.Parameters.AddWithValue("@mdpTechnicien", unTechnicien.Mdp);
            cmd.Parameters.AddWithValue("@idResponsable", unTechnicien.IdResponsable);
            cmd.ExecuteNonQuery();
            connec.Close();
        }


        /// <summary>
        /// Ajoute un nouvel utilisateur à la base de données.
        /// </summary>
        /// <param name="unUtilisateur">L'utilisateur à ajouter.</param>
        public static void ajoutUtilisateur(Utilisateur unUtilisateur)
        {
            // Ouvre la connexion à la base de données
            connec.Open();

            // Crée une commande SQL pour insérer un nouvel utilisateur
            MySqlCommand cmd = connec.CreateCommand();

            // Définir la commande SQL avec des paramètres
            cmd.CommandText = "INSERT INTO utilisateur(idUtilisateur, nomUtilisateur, prenomUtilisateur, mdpUtilisateur) values(@idUtilisateur,  @nomUtilisateur, @prenomUtilisateur,  @mdpUtilisateur)";

            // Ajoute les paramètres de la commande
            cmd.Parameters.AddWithValue("@idUtilisateur", unUtilisateur.Id);
            cmd.Parameters.AddWithValue("@nomUtilisateur", unUtilisateur.Nom);
            cmd.Parameters.AddWithValue("@prenomUtilisateur", unUtilisateur.Prenom);
            cmd.Parameters.AddWithValue("@mdpUtilisateur", unUtilisateur.Mdp);

            // Exécute la commande pour insérer l'utilisateur dans la base de données
            cmd.ExecuteNonQuery();

            // Ferme la connexion à la base de données
            connec.Close();
        }


        /// <summary>
        /// Récupère la liste de tous les utilisateurs depuis la base de données et la retourne.
        /// </summary>
        /// <returns>Liste des utilisateurs.</returns>
        public static List<Utilisateur> GetUtilisateurs()
        {
            // Crée une nouvelle liste pour stocker les utilisateurs récupérés
            lesUtilisateurs = new List<Utilisateur>();

            // Ouvre la connexion à la base de données
            connec.Open();

            // Crée une commande SQL pour récupérer tous les utilisateurs
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "SELECT * FROM utilisateur";

            // Exécute la commande et obtient les résultats dans un lecteur
            MySqlDataReader reader = cmd.ExecuteReader();

            // Parcourt chaque ligne du résultat
            while (reader.Read())
            {
                // Récupère les informations de l'utilisateur à partir du lecteur
                int idUtilisateur = reader.GetInt32("idUtilisateur");
                string nomUtilisateur = reader.GetString("nomUtilisateur");
                string prenomUtilisateur = reader.GetString("prenomUtilisateur");
                string mdpUtilisateur = reader.GetString("mdpUtilisateur");

                // Crée un objet Utilisateur avec les données récupérées
                Utilisateur unUtilisateur = new Utilisateur(idUtilisateur, nomUtilisateur, prenomUtilisateur, mdpUtilisateur);

                // Ajoute l'utilisateur à la liste
                lesUtilisateurs.Add(unUtilisateur);
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne la liste des utilisateurs
            return lesUtilisateurs;
        }


        /// <summary>
        /// Récupère la liste de tous les techniciens depuis la base de données et la retourne.
        /// </summary>
        /// <returns>Liste des techniciens.</returns>
        public static List<Technicien> GetTechniciens()
        {
            // Crée une nouvelle liste pour stocker les techniciens récupérés
            lesTechniciens = new List<Technicien>();

            // Crée une liste pour stocker les compétences des techniciens
            List<Competences> competencestech = new List<Competences>();

            // Récupère la liste des compétences disponibles depuis la base de données
            List<Competences> lesComp = Bd.GetCompetences();

            // Ouvre la connexion à la base de données
            connec.Open();

            // Crée une commande SQL pour récupérer tous les techniciens
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "SELECT * FROM technicien";

            // Exécute la commande et obtient les résultats dans un lecteur
            MySqlDataReader reader = cmd.ExecuteReader();

            // Parcourt chaque ligne du résultat
            while (reader.Read())
            {
                // Récupère les informations du technicien depuis le lecteur
                int idTechnicien = reader.GetInt32("idTechnicien");
                string nomTechnicien = reader.GetString("nomTechnicien");
                string prenomTechnicien = reader.GetString("prenomTechnicien");
                string matriculeTechnicien = reader.GetString("matriculeTechnicien");
                DateTime dateEmbPersonnel = reader.GetDateTime("dateEmbPersonnel");
                string niveauTechnicien = reader.GetString("niveauTechnicien");
                string competencesTechnicien = reader.GetString("competencesTechnicien");
                string formationTechnicien = reader.GetString("formationTechnicien");
                string mdpTechnicien = reader.GetString("mdpTechnicien");
                int idResponsable = reader.GetInt16("idResponsable");

                // Décompose les compétences associées au technicien et les compare avec celles de la base de données
                foreach (Competences competence in lesComp)
                {
                    char[] delimiteur = { ';' }; // Délimite les compétences par le caractère ';'
                    foreach (string comp in competencesTechnicien.Split(delimiteur))
                    {
                        // Si l'ID de la compétence correspond, l'ajoute à la liste des compétences du technicien
                        if (Convert.ToString(competence.Id) == comp)
                        {
                            competencestech.Add(competence);
                        }
                    }
                }

                // Crée un objet Technicien avec les données récupérées
                Technicien unTechnicien = new Technicien(idTechnicien, nomTechnicien, prenomTechnicien, matriculeTechnicien, dateEmbPersonnel, niveauTechnicien, competencestech, formationTechnicien, mdpTechnicien, idResponsable);

                // Ajoute le technicien à la liste des techniciens
                lesTechniciens.Add(unTechnicien);
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne la liste des techniciens
            return lesTechniciens;
        }

        /// <summary>
        /// Récupère tous les identifiants des utilisateurs.
        /// </summary>
        /// <returns>Une chaîne contenant tous les identifiants des utilisateurs.</returns>
        public static String GetIdUtilisateurs()
        {
            // Ouvre la connexion à la base de données
            connec.Open();

            // Crée une commande SQL pour récupérer les identifiants des utilisateurs
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "SELECT idUtilisateur FROM utilisateur";

            // Exécute la commande et obtient les résultats dans un lecteur
            MySqlDataReader reader = cmd.ExecuteReader();

            // Parcourt chaque ligne du résultat pour récupérer les identifiants
            while (reader.Read())
            {
                int idUtilisateur = reader.GetInt32("idUtilisateur");

                // Ajoute chaque identifiant à la chaîne avec un séparateur
                listIdUtilisateur += Convert.ToString(idUtilisateur) + ";";
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne la chaîne des identifiants
            return listIdUtilisateur;
        }

        /// <summary>
        /// Récupère tous les identifiants des techniciens.
        /// </summary>
        /// <returns>Une chaîne contenant tous les identifiants des techniciens.</returns>
        public static String GetIdTechniciens()
        {
            // Ouvre la connexion à la base de données
            connec.Open();

            // Crée une commande SQL pour récupérer les identifiants des techniciens
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "SELECT idTechnicien FROM technicien";

            // Exécute la commande et obtient les résultats dans un lecteur
            MySqlDataReader reader = cmd.ExecuteReader();

            // Parcourt chaque ligne du résultat pour récupérer les identifiants
            while (reader.Read())
            {
                int idTechnicien = reader.GetInt32("idTechnicien");

                // Ajoute chaque identifiant à la chaîne avec un séparateur
                listIdTechnicien += Convert.ToString(idTechnicien) + ";";
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne la chaîne des identifiants
            return listIdTechnicien;
        }

        /// <summary>
        /// Récupère la liste de toutes les compétences disponibles dans la base de données et la retourne.
        /// </summary>
        /// <returns>Liste des compétences.</returns>
        public static List<Competences> GetCompetences()
        {
            // Crée une nouvelle liste pour stocker les compétences récupérées
            lesCompetences = new List<Competences>();

            // Ouvre la connexion à la base de données
            connec.Open();

            // Crée une commande SQL pour récupérer toutes les compétences
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "SELECT * FROM competence";

            // Exécute la commande et obtient les résultats dans un lecteur
            MySqlDataReader reader = cmd.ExecuteReader();

            // Parcourt chaque ligne du résultat pour récupérer les compétences
            while (reader.Read())
            {
                int idCompetence = reader.GetInt32("idCompetence");
                string descriptionCompetence = reader.GetString("descriptionCompetence");

                // Crée un objet Competences avec les données récupérées
                Competences uneCompetence = new Competences(idCompetence, descriptionCompetence);

                // Ajoute la compétence à la liste
                lesCompetences.Add(uneCompetence);
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne la liste des compétences
            return lesCompetences;
        }

        /// <summary>
        /// Modifie les informations d'un technicien existant dans la base de données.
        /// </summary>
        /// <param name="unTechnicien">Le technicien dont les informations doivent être modifiées.</param>
        public static void modifTechnicien(Technicien unTechnicien)
        {
            // Ouvre la connexion à la base de données
            connec.Open();

            // Crée une commande SQL pour mettre à jour un technicien
            MySqlCommand cmd = connec.CreateCommand();

            // Initialise une variable pour stocker les compétences du technicien sous forme de chaîne
            competencebis = "";
            foreach (Competences competences in unTechnicien.Competences)
            {
                competencebis += competences.Id + ";"; // Ajoute chaque compétence à la chaîne
            }

            // Définir la commande SQL pour mettre à jour les informations du technicien
            cmd.CommandText = "UPDATE technicien SET nomTechnicien = @nomTechnicien, prenomTechnicien = @prenomTechnicien, matriculeTechnicien = @matriculeTechnicien, dateEmbPersonnel = @dateEmbPersonnel, niveauTechnicien = @niveauTechnicien, competencesTechnicien = @competencesTechnicien, formationTechnicien = @formationTechnicien, mdpTechnicien = @mdpTechnicien, idResponsable = @idResponsable WHERE idTechnicien = @id";

            // Ajouter les paramètres à la commande SQL
            cmd.Parameters.AddWithValue("@id", unTechnicien.Id);
            cmd.Parameters.AddWithValue("@nomTechnicien", unTechnicien.Nom);
            cmd.Parameters.AddWithValue("@prenomTechnicien", unTechnicien.Prenom);
            cmd.Parameters.AddWithValue("@matriculeTechnicien", unTechnicien.Matricule);
            cmd.Parameters.AddWithValue("@dateEmbPersonnel", unTechnicien.DateEmbauche);
            cmd.Parameters.AddWithValue("@niveauTechnicien", unTechnicien.Niveau);
            cmd.Parameters.AddWithValue("@competencesTechnicien", competencebis);
            cmd.Parameters.AddWithValue("@formationTechnicien", unTechnicien.Formation);
            cmd.Parameters.AddWithValue("@mdpTechnicien", unTechnicien.Mdp);
            cmd.Parameters.AddWithValue("@idResponsable", unTechnicien.IdResponsable);

            // Exécute la commande pour mettre à jour les données du technicien
            cmd.ExecuteNonQuery();

            // Ferme la connexion à la base de données
            connec.Close();
        }

        /// <summary>
        /// Modifie les informations d'un utilisateur existant dans la base de données.
        /// </summary>
        /// <param name="unUtilisateur">L'utilisateur dont les informations doivent être modifiées.</param>
        public static void modifUtilisateur(Utilisateur unUtilisateur)
        {
            // Ouvre la connexion à la base de données
            connec.Open();

            // Crée une commande SQL pour mettre à jour un utilisateur
            MySqlCommand cmd = connec.CreateCommand();

            // Définir la commande SQL pour mettre à jour les informations de l'utilisateur
            cmd.CommandText = "UPDATE utilisateur SET  nomUtilisateur = @nomUtilisateur, prenomUtilisateur = @prenomUtilisateur, mdpUtilisateur = @mdpUtilisateur WHERE idUtilisateur = @id";

            // Ajouter les paramètres à la commande SQL
            cmd.Parameters.AddWithValue("@id", unUtilisateur.Id);
            cmd.Parameters.AddWithValue("@nomUtilisateur", unUtilisateur.Nom);
            cmd.Parameters.AddWithValue("@prenomUtilisateur", unUtilisateur.Prenom);
            cmd.Parameters.AddWithValue("@mdpUtilisateur", unUtilisateur.Mdp);

            // Exécute la commande pour mettre à jour les données de l'utilisateur
            cmd.ExecuteNonQuery();

            // Ferme la connexion à la base de données
            connec.Close();
        }

        /// <summary>
        /// Supprime un technicien de la base de données en fonction de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant du technicien à supprimer.</param>
        public static void supprimerTechnicien(int id)
        {
            // Ouvre la connexion à la base de données
            connec.Open();

            // Crée une commande SQL pour supprimer un technicien
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "DELETE FROM Technicien WHERE idTechnicien = @id";

            // Ajouter le paramètre à la commande SQL
            cmd.Parameters.AddWithValue("@id", id);

            // Exécute la commande pour supprimer le technicien
            cmd.ExecuteNonQuery();

            // Ferme la connexion à la base de données
            connec.Close();
        }

        /// <summary>
        /// Supprime un utilisateur de la base de données en fonction de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de l'utilisateur à supprimer.</param>
        public static void supprimerUtilisateur(int id)
        {
            // Ouvre la connexion à la base de données
            connec.Open();

            // Crée une commande SQL pour supprimer un utilisateur
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "DELETE FROM utilisateur WHERE idUtilisateur = @id";

            // Ajouter le paramètre à la commande SQL
            cmd.Parameters.AddWithValue("@id", id);

            // Exécute la commande pour supprimer l'utilisateur
            cmd.ExecuteNonQuery();

            // Ferme la connexion à la base de données
            connec.Close();
        }


        /// <summary>
        /// Récupère le dernier identifiant utilisé parmi les utilisateurs, responsables et techniciens, et l'incrémente de 1.
        /// </summary>
        /// <returns>Le dernier identifiant incrémenté.</returns>
        public static int dernierIdUtilisateur()
        {
            int dernier = 1;
            string query = @"
    SELECT GREATEST(
        (SELECT IFNULL(MAX(idResponsable), 0) FROM responsable),
        (SELECT IFNULL(MAX(idUtilisateur), 0) FROM utilisateur),
        (SELECT IFNULL(MAX(idTechnicien), 0) FROM technicien)
    )";

            // Ouvre la connexion à la base de données
            connec.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, connec))
            {
                // Récupère le plus grand identifiant parmi les utilisateurs, responsables et techniciens
                dernier += Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne l'identifiant incrémenté
            return dernier;
        }

        /// <summary>
        /// Calcule et retourne le nombre total d'utilisateurs, de responsables et de techniciens dans la base de données.
        /// </summary>
        /// <returns>Le nombre total d'utilisateurs, responsables et techniciens.</returns>
        public static int StatTotalUtilisateur()
        {
            int dernier = 0;
            string query = @"
    SELECT (SELECT COUNT(*) FROM responsable) +
    (SELECT COUNT(*) FROM utilisateur) +
    (SELECT COUNT(*) FROM technicien)";

            // Ouvre la connexion à la base de données
            connec.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, connec))
            {
                // Récupère et additionne le nombre d'utilisateurs, responsables et techniciens
                dernier = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne le nombre total d'utilisateurs
            return dernier;
        }

        /// <summary>
        /// Calcule et retourne le nombre total d'incidents ayant le statut "Résolu".
        /// </summary>
        /// <returns>Le nombre total d'incidents résolus.</returns>
        public static int TotalResolu()
        {
            int dernier = 0;
            string query = @"SELECT COUNT(*) FROM incident WHERE statutIncident = 'Résolu' ";

            // Ouvre la connexion à la base de données
            connec.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, connec))
            {
                // Récupère le nombre d'incidents ayant le statut "Résolu"
                dernier = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne le nombre d'incidents résolus
            return dernier;
        }

        /// <summary>
        /// Calcule et retourne le nombre total d'incidents ayant un statut commençant par "Pri" (pris en charge).
        /// </summary>
        /// <returns>Le nombre total d'incidents pris en charge.</returns>
        public static int TotalPEC()
        {
            int dernier = 0;
            string query = @"SELECT COUNT(*) FROM incident WHERE statutIncident LIKE 'Pri%' ";

            // Ouvre la connexion à la base de données
            connec.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, connec))
            {
                // Récupère le nombre d'incidents pris en charge (statut commençant par "Pri")
                dernier = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne le nombre d'incidents pris en charge
            return dernier;
        }

        /// <summary>
        /// Récupère le dernier identifiant utilisé pour les incidents en ajoutant le nombre d'incidents existants.
        /// </summary>
        /// <returns>Le dernier identifiant d'incident incrémenté.</returns>
        public static int dernierIdIncident()
        {
            int dernier = 1;
            string query = "SELECT COUNT(*) FROM incident";

            // Ouvre la connexion à la base de données
            connec.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, connec))
            {
                // Récupère le nombre d'incidents existants et l'ajoute à 1
                dernier = dernier + Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne le dernier identifiant d'incident
            return dernier;
        }

        /// <summary>
        /// Récupère le dernier identifiant utilisé pour les interventions en ajoutant le nombre d'interventions existantes.
        /// </summary>
        /// <returns>Le dernier identifiant d'intervention incrémenté.</returns>
        public static int dernierIdIntervention()
        {
            int dernier = 1;
            string query = "SELECT COUNT(*) FROM intervention";

            // Ouvre la connexion à la base de données
            connec.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, connec))
            {
                // Récupère le nombre d'interventions existantes et l'ajoute à 1
                dernier = dernier + Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne le dernier identifiant d'intervention
            return dernier;
        }

        /// <summary>
        /// Récupère le dernier identifiant utilisé pour les fournisseurs en ajoutant le nombre de fournisseurs existants.
        /// </summary>
        /// <returns>Le dernier identifiant de fournisseur incrémenté.</returns>
        public static int dernierIdFournisseur()
        {
            int dernier = 1;
            string query = "SELECT COUNT(*) FROM fournisseur";

            // Ouvre la connexion à la base de données
            connec.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, connec))
            {
                // Récupère le nombre de fournisseurs existants et l'ajoute à 1
                dernier = dernier + Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne le dernier identifiant de fournisseur
            return dernier;
        }

        /// <summary>
        /// Récupère le dernier identifiant utilisé pour les techniciens en ajoutant le nombre de techniciens existants.
        /// </summary>
        /// <returns>Le dernier identifiant de technicien incrémenté.</returns>
        public static int dernierIdTechnicien()
        {
            int dernier = 1;
            string query = "SELECT COUNT(*) FROM technicien";

            // Ouvre la connexion à la base de données
            connec.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, connec))
            {
                // Récupère le nombre de techniciens existants et l'ajoute à 1
                dernier = dernier + Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne le dernier identifiant de technicien
            return dernier;
        }

        /// <summary>
        /// Récupère le dernier identifiant utilisé pour les compétences en ajoutant le nombre de compétences existantes.
        /// </summary>
        /// <returns>Le dernier identifiant de compétence incrémenté.</returns>
        public static int dernierIdCompetence()
        {
            int dernier = 1;
            string query = "SELECT COUNT(*) FROM competence";

            // Ouvre la connexion à la base de données
            connec.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, connec))
            {
                // Récupère le nombre de compétences existantes et l'ajoute à 1
                dernier = dernier + Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Ferme la connexion à la base de données
            connec.Close();

            // Retourne le dernier identifiant de compétence
            return dernier;
        }

        /// <summary>
        /// Récupère et retourne la liste de tous les fournisseurs depuis la base de données.
        /// </summary>
        /// <returns>La liste des fournisseurs.</returns>
        public static List<Fournisseur> GetFournisseurs()
        {
            lesFournisseurs = new List<Fournisseur>();
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "SELECT * FROM fournisseur";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Fournisseur unFournisseur = new Fournisseur((int)reader["idFournisseur"], (string)reader["nomFournisseur"], (string)reader["adresseFournisseur"]);
                lesFournisseurs.Add(unFournisseur);
            }
            connec.Close();
            return lesFournisseurs;
        }

        /// <summary>
        /// Ajoute un fournisseur dans la base de données en insérant ses informations.
        /// </summary>
        /// <param name="unFournisseur">L'objet Fournisseur à ajouter.</param>
        public static void ajoutFournisseur(Fournisseur unFournisseur)
        {
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "INSERT INTO fournisseur(idFournisseur, nomFournisseur, adresseFournisseur) values(@idFournisseur, @nomFournisseur, @adresseFournisseur)";
            cmd.Parameters.AddWithValue("@idFournisseur", unFournisseur.Id);
            cmd.Parameters.AddWithValue("@nomFournisseur", unFournisseur.Nom);
            cmd.Parameters.AddWithValue("@adresseFournisseur", unFournisseur.Adresse);
            cmd.ExecuteNonQuery();
            connec.Close();
        }

        /// <summary>
        /// Calcule et retourne le nombre moyen de jours pour la résolution d'un incident.
        /// </summary>
        /// <returns>Le nombre moyen de jours pour résoudre un incident.</returns>
        public static double nbJourMoyenDintervention()
        {
            double nbJourMoyen = 0;
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "SELECT AVG(tempsResolutionIncident) FROM incident";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    nbJourMoyen = reader.GetDouble(0);
                }
            }
            connec.Close();
            return nbJourMoyen;
        }

        /// <summary>
        /// Récupère et retourne la liste de tous les incidents associés à un utilisateur spécifique.
        /// </summary>
        /// <param name="id">L'identifiant de l'utilisateur.</param>
        /// <returns>La liste des incidents de l'utilisateur.</returns>
        public static List<Incident> GetIncidentUtilisateur(int id)
        {
            lesIncidentsUtilisateur = new List<Incident>();
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "SELECT * FROM incident WHERE idUtilisateur = @id";
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idIncident = reader.GetInt32("idIncident");
                string descriptionIncident = reader.GetString("descriptionIncident");
                DateTime dateDeclarationIncident = reader.GetDateTime("dateDeclarationIncident");
                string urgenceIncident = reader.GetString("urgenceIncident");
                string statutIncident = reader.GetString("statutIncident");

                // Champs pouvant être NULL
                string travailEffectue = reader.IsDBNull(reader.GetOrdinal("travailEffectue"))
                    ? string.Empty
                    : reader.GetString("travailEffectue");

                DateTime? dateTraitementIncident = reader.IsDBNull(reader.GetOrdinal("dateTraitementIncident"))
                    ? (DateTime?)null
                    : reader.GetDateTime("dateTraitementIncident");

                int? tempsResolutionIncident = reader.IsDBNull(reader.GetOrdinal("tempsResolutionIncident"))
                    ? (int?)null
                    : reader.GetInt32("tempsResolutionIncident");

                // Champs non NULL
                string posteIncident = reader.GetString("posteIncident");
                int idMateriel = reader.GetInt32("idMateriel");
                int idUtilisateur = reader.GetInt32("idUtilisateur");

                // Crée un nouvel objet Incident avec les valeurs récupérées
                Incident unIncident = new Incident(
                    idIncident,
                    descriptionIncident,
                    dateDeclarationIncident,
                    urgenceIncident,
                    statutIncident,
                    travailEffectue,
                    dateTraitementIncident,
                    tempsResolutionIncident,
                    posteIncident,
                    idMateriel,
                    idUtilisateur
                );
                lesIncidentsUtilisateur.Add(unIncident);
            }
            connec.Close();
            return lesIncidentsUtilisateur;
        }


        public static void ajoutChercheur(Chercheur unChercheur)
        {
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "INSERT INTO chercheur(nom, prenom, spe, anne_these) values(@nomChercheur, @prenomChercheur, @speChercheur, @anneeChercheur)";
            cmd.Parameters.AddWithValue("@nomChercheur", unChercheur.Nom);
            cmd.Parameters.AddWithValue("@prenomChercheur", unChercheur.Prenom);
            cmd.Parameters.AddWithValue("@speChercheur", unChercheur.Specialite);
            cmd.Parameters.AddWithValue("@anneeChercheur", unChercheur.Annee);
            cmd.ExecuteNonQuery();
            connec.Close();
        }

        public static List<Chercheur> GetChercheur()
        {
            lesChercheur = new List<Chercheur>();
            connec.Open();
            MySqlCommand cmd = connec.CreateCommand();
            cmd.CommandText = "SELECT * FROM chercheur";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Chercheur unChercheur = new Chercheur((int)reader["id"], (string)reader["nom"], (string)reader["prenom"], (string)reader["spe"], (string)reader["anne_these"]);
                lesChercheur.Add(unChercheur);
            }
            connec.Close();
            return lesChercheur;
        }


    }
}
