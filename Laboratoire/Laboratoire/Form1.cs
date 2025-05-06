 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratoire
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int connexion;
        int dernierIdUtilisateur;
        int dernierIdIncident;
        int dernierIdIntervention;
        int dernierIdFournisseur;
        int dernierIdCompetence;
        int pos;
        string listIdTechnicien;
        string listIdUtilisateur;
        List<Utilisateur> lesUtilisateurs;
        List<Incident> lesIncidents;
        List<Incident> lesIncidentsUtilisateur;
        List<Fournisseur> lesFournisseurs;
        List<Materiel> lesMateriels;
        List<Technicien> lesTechniciens;
        List <Competences> lesCompetences;
        List<Chercheur> lesChercheur;
        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
            tabControl1.TabPages.Remove(tabPage4);
            tabControl1.TabPages.Remove(tabPage5);
            tabControl1.TabPages.Remove(tabPage6);
            tabControl1.TabPages.Remove(tabPage7);
            dernierIdUtilisateur = Bd.dernierIdUtilisateur();
            dernierIdIncident = Bd.dernierIdIncident();
            lesIncidents = Bd.GetIncidents();
            lesUtilisateurs = Bd.GetUtilisateurs();
            lesFournisseurs = Bd.GetFournisseurs();
            lesChercheur = Bd.GetChercheur();
            lesMateriels = Bd.GetMateriels();
            lesCompetences = Bd.GetCompetences();
            dernierIdIntervention = Bd.dernierIdIntervention();
            dernierIdFournisseur = Bd.dernierIdFournisseur();
            dernierIdCompetence = Bd.dernierIdCompetence();
            listIdUtilisateur = Bd.GetIdUtilisateurs();
            listIdTechnicien = Bd.GetIdTechniciens();
            lesTechniciens = Bd.GetTechniciens();
            foreach (Utilisateur unUtilisateur in lesUtilisateurs) 
            {
                comboUtilisateurIncident.Items.Add(unUtilisateur.Id);
                comboBoxUtilisateurMateriel.Items.Add(unUtilisateur.Id);
            }
            foreach (Materiel unMateriel in lesMateriels) 
            {
                comboMaterielIncident.Items.Add(unMateriel.Id);
            }
            foreach (Fournisseur unFournisseur in lesFournisseurs) 
            {
                comboBoxFournisseurMateriel.Items.Add(unFournisseur.Id);
            }
            listIdUtilisateur = listIdUtilisateur.Remove(listIdUtilisateur.Length -1);
            listIdTechnicien = listIdTechnicien.Remove(listIdTechnicien.Length - 1);
            char[] delimiteur = { ';' };
            foreach (String idUtil in listIdUtilisateur.Split(delimiteur))
            {
                comboBoxSupprimerUser.Items.Add(idUtil + ";Utilisateur" );
                comboBoxModifierIdUtil.Items.Add(idUtil + ";Utilisateur");
            }
            foreach (String idTech in listIdTechnicien.Split(delimiteur))
            {
                comboBoxSupprimerUser.Items.Add(idTech + ";Technicien" );
                comboBoxModifierIdUtil.Items.Add(idTech + ";Technicien" );
            }
        }
        /// <summary>
        /// Ce bouton est déclenché lors de la tentative de connexion d'un utilisateur à l'application.
        /// Il vérifie les identifiants (nom d'utilisateur et mot de passe), puis affiche les pages correspondantes
        /// en fonction du rôle et des autorisations de l'utilisateur. Selon le rôle, l'accès à certaines pages
        /// (tabPages) sera restreint, et les incidents non résolus peuvent être affichés pour l'utilisateur connecté.
        /// </summary>
        private void btnConnexion_Click(object sender, EventArgs e)
        {       //Ce bouton permet de connecter un utilisateur a l'application et d'avoir accés a seulement les pages nécessaire a ces droit et ces autorisation
            connexion = Bd.connexion(tbIdConnexion.Text, tbMdpConnexion.Text);
            if (connexion == 0)
            {
                MessageBox.Show("Nom d'utilisateur / Mot de passe incorrecte");
            }
            else
                tabControl1.TabPages.Remove(tabPage1);
            {
                if (connexion == 1) 
                {
                    tabControl1.TabPages.Add(tabPage2);
                    lesIncidentsUtilisateur = Bd.GetIncidentUtilisateur(Convert.ToInt16(tbIdConnexion.Text));
                    foreach (Incident unIncident in lesIncidentsUtilisateur) 
                    {
                        if (unIncident.StatutIncident != "Résolu")
                        {
                            lbIncidentUtilisateur.Items.Add(unIncident.Id);
                        }
                    }
                }
                if (connexion == 2)
                {
                    tabControl1.TabPages.Add(tabPage3);
                    tabControl1.TabPages.Add(tabPage4);
                }
                if (connexion == 3)
                {
                    tabControl1.TabPages.Add(tabPage5);
                    tabControl1.TabPages.Add(tabPage6);
                    tabControl1.TabPages.Add(tabPage7);
                }
            }
        }

        /// <summary>
        /// Ce bouton permet de déclarer un incident pour un utilisateur. Il récupère les informations fournies par l'utilisateur
        /// (description, importance, matériel et utilisateur), crée un nouvel objet `Incident` et l'ajoute à la base de données.
        /// Le statut de l'incident est initialisé à "Déclaré", et un identifiant unique est généré pour chaque incident.
        /// </summary>

        private void buttonIncident_Click(object sender, EventArgs e)
        {  // permet de déclarer un incident pour un utilisateur
            Incident unIncident;
            string importance;
            if (radioPetitPB.Checked)
            {
                importance = "Petite";
            }
            else 
            {
                if (radioMoyenPB.Checked)
                {
                    importance = "Moyenne";
                }
                else 
                {
                    importance = "Grave";
                }
            }
            unIncident = new Incident(dernierIdIncident, rtfDescriptionPB.Text, DateTime.Now, importance, "Déclaré", "", DateTime.MinValue, 0, tbPostePB.Text, Convert.ToInt16(comboMaterielIncident.SelectedItem.ToString()), Convert.ToInt16(comboUtilisateurIncident.SelectedItem.ToString()));
            Bd.ajoutIncident(unIncident);
            lesIncidents.Add(unIncident);
            dernierIdIncident++;
        }

        private void buttonEnregistrerNewMT_Click(object sender, EventArgs e)
        {       // Permet d'enregistrer un  nouveaux matériaux
           

            Materiel unMateriel;
            int idMateriel = Convert.ToInt16(textBoxIdMT.Text);
            int utilisateurId = Convert.ToInt16(comboBoxUtilisateurMateriel.SelectedItem.ToString());
            int fournisseurId = Convert.ToInt16(comboBoxFournisseurMateriel.SelectedItem.ToString());

            if (radioButtonAchatMT.Checked)
            {
                if (radioButtonOuiMateriel.Checked)
                {
                    unMateriel = new Materiel(idMateriel, textBoxCaractMT.Text, dateTimePickerAchat.Value, null, dateTimePickerGarentie.Value, utilisateurId, fournisseurId);
                }
                else
                {
                    unMateriel = new Materiel(idMateriel, textBoxCaractMT.Text, dateTimePickerAchat.Value, null, null, utilisateurId, fournisseurId);
                }
            }
            else
            {
                if (radioButtonOuiMateriel.Checked)
                {
                    unMateriel = new Materiel(idMateriel, textBoxCaractMT.Text, null, dateTimePickerAchat.Value, dateTimePickerGarentie.Value, utilisateurId, fournisseurId);
                }
                else
                {
                    unMateriel = new Materiel(idMateriel, textBoxCaractMT.Text, null, dateTimePickerAchat.Value, null, utilisateurId, fournisseurId);
                }
            }
            Bd.ajoutMateriel(unMateriel);
            lesMateriels.Add(unMateriel);
            comboMaterielIncident.Items.Add(unMateriel);
        }
        /// <summary>
        /// Ce bouton permet d'afficher une liste de tous les matériels existants dans l'application. 
        /// Les matériels sont extraits de la liste `lesMateriels` et leurs identifiants sont ajoutés à la `listBoxConsulterMT`.
        /// </summary>

        private void buttonConsulterMT_Click(object sender, EventArgs e)
        {       // Permet d'affichier une liste des matériaux excistant
            listBoxConsulterMT.Items.Clear();
            foreach (Materiel unMateriel in lesMateriels) 
            {
                listBoxConsulterMT.Items.Add(unMateriel.Id);
            }
        }

        /// <summary>
        /// Ce bouton permet de supprimer un matériel de la liste, sélectionné dans la `listBoxConsulterMT`. 
        /// Le matériel est d'abord supprimé de la base de données via la méthode `Bd.supprimerMateriel`, 
        /// puis retiré de la liste `lesMateriels`. La liste des matériels est ensuite actualisée.
        /// </summary>
        private void buttonSupprimerMT_Click(object sender, EventArgs e)
        {       // Permet de supprimer un matérielle de la liste
            Bd.supprimerMateriel(lesMateriels[listBoxConsulterMT.SelectedIndex]);
            lesMateriels.RemoveAt(listBoxConsulterMT.SelectedIndex);
            buttonConsulterMT_Click(sender,e);
        }

        /// <summary>
        /// Ce bouton permet de consulter la liste des incidents non résolus. 
        /// Tous les incidents sont parcourus dans la liste `lesIncidents`. 
        /// Si l'incident n'a pas le statut "Résolu", son identifiant est ajouté à la `listBoxIncident`.
        /// </summary>
        private void buttonConsulterIN_Click(object sender, EventArgs e)
        {       // Permet de consulter les incidents
            listBoxIncident.Items.Clear();
            foreach (Incident unIncident in lesIncidents) 
            {
                if (unIncident.StatutIncident != "Résolu")
                {
                    listBoxIncident.Items.Add(unIncident.Id);
                }
            }
        }
        /// <summary>
        /// Ce bouton permet de prendre en charge un incident en fonction de la méthode choisie par l'utilisateur.
        /// L'utilisateur peut sélectionner entre trois options : télémaintenance, prise en charge par téléphone, ou déplacement sur site.
        /// Le statut de l'incident est mis à jour en fonction de la sélection.
        /// Une nouvelle intervention est également créée et enregistrée dans la base de données.
        /// </summary>
        private void buttonPEC_Click(object sender, EventArgs e)
        {       // Permet de prendre en charges un incident
            Incident unIncident = lesIncidents[listBoxIncident.SelectedIndex];
            if (radioButtonTelemaintenanceIncident.Checked)
            {
                unIncident.StatutIncident = "Prise en charge par un technicien en télémaintenance";
            }
            else 
            {
                if (radioButtonTelephoneIncident.Checked)
                {
                    unIncident.StatutIncident = "Prise en charge par un technicien au téléphone";
                }
                else 
                {
                    unIncident.StatutIncident = "Déplacement sur site";
                }
            }
            Bd.modifIncident(unIncident);
            Intervention uneIntervention = new Intervention(dernierIdIntervention, "", unIncident.StatutIncident, Convert.ToInt16(tbIdConnexion.Text), unIncident.Id);
            Bd.priseEnChargeIncident(uneIntervention);
            dernierIdIntervention++;
        }

        /// <summary>
        /// Ce bouton permet de modifier un incident sélectionné dans la liste.
        /// L'utilisateur peut renseigner les travaux effectués et mettre à jour le statut de l'incident.
        /// Si l'incident est résolu, le temps de résolution et la date de traitement sont également mis à jour.
        /// </summary>

        private void buttonModifierIN_Click(object sender, EventArgs e)
        {       // Permet de modifier un incident
            Incident incident = lesIncidents[listBoxIncident.SelectedIndex];
            incident.TravailEffectue = rtbTravailEffectueIncident.Text;
            if (radioButtonResoluIncidentOui.Checked)
            {
                incident.TempsResolutionIncident = Convert.ToInt16(DateTime.Now - incident.DateDeclarationIncident);
                incident.DateTraitementIncident = DateTime.Now;
                incident.StatutIncident = "Résolu";
            }
            else 
            {
                incident.StatutIncident = textBoxNewStatutIN.Text;
            }
            Bd.modifIncident(incident);
        }

        /// <summary>
        /// Ce bouton permet d'ajouter un fournisseur dans la base de données.
        /// Les informations du fournisseur sont récupérées depuis les champs de texte et un nouveau fournisseur est créé.
        /// </summary>

        private void btnAjoutFournisseur_Click(object sender, EventArgs e)
        {  // permet d'ajouter un fournisseur a la liste des fournisseur
            Bd.ajoutFournisseur(new Fournisseur(dernierIdFournisseur, textBoxNomFournisseur.Text, textBoxAdresseFournisseur.Text));
        }

        /// <summary>
        /// Ce bouton permet d'ajouter un utilisateur dans la base de données.
        /// Si l'utilisateur est un simple utilisateur, il est ajouté à la base avec les informations saisies.
        /// Si l'utilisateur est un technicien, ses compétences sont également ajoutées, et le technicien est enregistré avec un identifiant, un niveau de compétence et d'autres détails spécifiques.
        /// </summary>

        private void buttonAjoutUtilisateur_Click(object sender, EventArgs e)
        {   // Permet d'ajouter un utilisateur 
            List<Competences> lesCompetencesAjouter = new List<Competences>();
            if (radioButtonUtilisateurAJ.Checked)
            {
                Bd.ajoutUtilisateur(new Utilisateur(dernierIdUtilisateur, textBoxNomAdd.Text, textBoxPrenomAdd.Text, textBoxMdpAdd.Text));
                dernierIdUtilisateur++;
            }
            else 
            {
                char[] delimiteur = { ';' };
                foreach (String comp in textBoxComptAdd.Text.Split(delimiteur)) 
                {
                    lesCompetencesAjouter.Add(lesCompetences[Convert.ToInt16(comp)-1]);
                }
                Bd.ajoutTechnicien(new Technicien(dernierIdUtilisateur, textBoxNomAdd.Text, textBoxPrenomAdd.Text, textBoxMatriculeAdd.Text, DateTime.Now, textBoxNivAdd.Text, lesCompetences, textBoxFormaAdd.Text, textBoxMdpAdd.Text, Convert.ToInt16(tbIdConnexion.Text)));
            }
        }

        /// <summary>
        /// Ce bouton permet de supprimer un utilisateur ou un technicien de la base de données.
        /// Le nom et le type de l'utilisateur sont récupérés à partir d'un élément sélectionné dans le comboBox.
        /// En fonction du type (Utilisateur ou Technicien), l'utilisateur ou technicien correspondant est supprimé.
        /// </summary>
        private void buttonSupprimer_Click(object sender, EventArgs e)
        {           // Permet de supprimer un users du logiciel
            string[] a = comboBoxSupprimerUser.SelectedItem.ToString().Split(';');
            if (a[1] == "Utilisateur") 
            {
                Bd.supprimerUtilisateur(Convert.ToInt16(a[0]));
                comboBoxSupprimerUser.Items.RemoveAt(comboBoxSupprimerUser.SelectedIndex);
            }
            else
            {
                Bd.supprimerTechnicien(Convert.ToInt16(a[0]));
                comboBoxSupprimerUser.Items.RemoveAt(comboBoxSupprimerUser.SelectedIndex);
            }
        }

        /// <summary>
        /// Ce bouton permet de modifier un utilisateur ou un technicien dans le logiciel.
        /// Selon le type d'utilisateur sélectionné (Utilisateur ou Technicien), les informations correspondantes sont mises à jour dans la base de données.
        /// Les champs modifiables incluent le nom, prénom, mot de passe, et pour les techniciens, des informations supplémentaires comme le matricule, le niveau, la formation, et les compétences.
        /// </summary>
        private void buttonModifierUtilisateurs_Click(object sender, EventArgs e)
        {           // Permet de modifier un utilisateur du logiciel et ces informations
            List<Competences> lesCompetencesAjouter = new List<Competences>();
            string[] a = comboBoxModifierIdUtil.SelectedItem.ToString().Split(';');
            if (a[1] == "Utilisateur")
            {
                Utilisateur unUtilisateur = lesUtilisateurs[comboBoxModifierIdUtil.SelectedIndex];
                if (textBoxNomModifierUtilisateur.Text != "") 
                {
                    unUtilisateur.Nom = textBoxNomModifierUtilisateur.Text;
                }
                if (textBoxPrenomModifierUtilisateur.Text != "") 
                {
                    unUtilisateur.Prenom = textBoxPrenomModifierUtilisateur.Text;
                }
                if (textBoxModifierMdpUtilisateur.Text != "") 
                {
                    unUtilisateur.Mdp = textBoxModifierMdpUtilisateur.Text;
                }
                Bd.modifUtilisateur(unUtilisateur);
            }
            else 
            {
                foreach (Technicien unTech in lesTechniciens) 
                {
                    if (unTech.Id == Convert.ToInt16(a[0]))
                    {
                        pos = lesTechniciens.IndexOf(unTech);
                    }
                    
                }
                Technicien unTechnicien = lesTechniciens[pos];
                if (textBoxNomModifierUtilisateur.Text != "")
                {
                    unTechnicien.Nom = textBoxNomModifierUtilisateur.Text;
                }
                if (textBoxPrenomModifierUtilisateur.Text != "")
                {
                    unTechnicien.Prenom = textBoxPrenomModifierUtilisateur.Text;
                }
                if (textBoxModifierMdpUtilisateur.Text != "")
                {
                    unTechnicien.Mdp = textBoxModifierMdpUtilisateur.Text;
                }
                if (textBoxModifierFormUtilisateur.Text != "")
                {
                    unTechnicien.Formation = textBoxModifierFormUtilisateur.Text;
                }
                if (textBoxModifierMatUtilisateur.Text != "")
                {
                    unTechnicien.Matricule = textBoxModifierMatUtilisateur.Text;
                }
                if (textBoxModifierNiveauUtilisateur.Text != "")
                {
                    unTechnicien.Niveau = textBoxModifierNiveauUtilisateur.Text;
                }
                if (textBoxModiferCompUtilisateur.Text != "")
                {
                    char[] delimiteur = { ';' };
                    foreach (String comp in textBoxModiferCompUtilisateur.Text.Split(delimiteur))
                    {
                        if (comp != "")
                        {
                            lesCompetencesAjouter.Add(lesCompetences[Convert.ToInt16(comp)-1]);
                        }
                    }
                    unTechnicien.Competences = lesCompetencesAjouter;
                }
                Bd.modifTechnicien(unTechnicien);
            }
        }

        /// <summary>
        /// Affiche les statistiques des incidents envoyés et du nombre total d'utilisateurs.
        /// Les statistiques sont affichées dans les labels correspondants :
        // - Le nombre total d'utilisateurs
        // - Le nombre total d'incidents envoyés
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {   // Quand on clique sa affiche les stats des incident envoyer et des nombre d'utilisateur 
            labelNbUsers.Text = Convert.ToString(Bd.StatTotalUtilisateur());
            labelIncidentEnvoie.Text = Convert.ToString(Bd.dernierIdIncident()-1);
        }

        /// <summary>
        /// Affiche les statistiques des techniciens concernant les incidents résolus et pris en charge.
        /// Les statistiques sont affichées dans les labels correspondants :
        // - Le nombre d'incidents résolus par les techniciens
        // - Le nombre d'incidents pris en charge par les techniciens
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {   // Permet d'avoir les stats technicien des incident résolut et pris en charge
            labelIncidentTech.Text = Convert.ToString(Bd.TotalResolu());
            labelChargeTech.Text = Convert.ToString(Bd.TotalPEC());
        }

        /// <summary>
        /// Affiche les statistiques des incidents déclarés, résolus et pris en charge, ainsi que la moyenne des jours pour les résoudre.
        /// Les statistiques sont affichées dans les labels correspondants :
        // - Le nombre d'incidents déclarés
        // - Le nombre d'incidents résolus
        // - Le nombre d'incidents pris en charge
        // - La moyenne des jours pour résoudre un incident
        /// </summary>
        private void buttonStatIncident_Click(object sender, EventArgs e)
        { //  Permet d'avoir les stats des incident déclarer, résolut , pris en charge et la moyenne des jours pour les résoudre
            labelDeclarer.Text = Convert.ToString(Bd.dernierIdIncident()-Bd.TotalResolu());
            labelResolut.Text = Convert.ToString(Bd.TotalResolu());
            labelCharges.Text = Convert.ToString(Bd.TotalPEC());
            labelJoursInter.Text = Convert.ToString(Bd.nbJourMoyenDintervention());
        }

        /// <summary>
        /// Affiche le statut et la description d'un incident sélectionné par l'utilisateur dans la liste.
        /// Les informations affichées sont mises à jour dans les champs de texte correspondants :
        // - Le statut de l'incident
        // - La description de l'incident
        /// </summary>
        private void lbIncidentUtilisateur_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtfStatutIncident.Text = lesIncidentsUtilisateur[lbIncidentUtilisateur.SelectedIndex].StatutIncident;
            rtfDescIncident.Text = lesIncidentsUtilisateur[lbIncidentUtilisateur.SelectedIndex].Descriptionincident;
        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void buttonAjoutChercheur_Click(object sender, EventArgs e)
        {
            Chercheur nouveauChercheur = new Chercheur(0, textBoxNomChercheur.Text, textBoxPrenomChercheur.Text, textBoxSpeChercheur.Text, textBoxAnneThese.Text);
            Bd.ajoutChercheur(nouveauChercheur);

            lesChercheur = Bd.GetChercheur();

            listBoxChercheur.Items.Clear();
            foreach (Chercheur unChercheur in lesChercheur)
            {
                listBoxChercheur.Items.Add(unChercheur.Nom);
            }


        }
    }
}
