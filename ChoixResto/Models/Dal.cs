using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace ChoixResto.Models
{
    public class Dal : IDal
    {
        private BddContext bdd;

        public Dal()
        {
            bdd = new BddContext();
        }

        public List<Resto> ObtientTousLesRestaurants()
        {
            return bdd.Restos.ToList();
        }


        public void CreerRestaurant(string nom, string telephone)
        {
            bdd.Restos.Add(new Resto { Nom = nom, Telephone = telephone });
            bdd.SaveChanges();
        }

        public void ModifierRestaurant(int id, string nom, string telephone)
        {
            Resto restoTrouve = bdd.Restos.FirstOrDefault(resto => resto.Id == id);
            if (restoTrouve != null)
            {
                restoTrouve.Nom = nom;
                restoTrouve.Telephone = telephone;
                bdd.SaveChanges();
            }

        }

        public bool RestaurantExiste(string nom)
        {
            Resto restoTrouve = bdd.Restos.FirstOrDefault(resto => resto.Nom == nom);

            if (restoTrouve != null)
                return true;
            return false;
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return bdd.Utilisateurs.FirstOrDefault(user => user.Id == id);
        }

        public Utilisateur ObtenirUtilisateur(string IdStr)
        {
            int id;
            if (int.TryParse(IdStr, out id))
                return ObtenirUtilisateur(id);
            return null;
        }

        public int AjouterUtilisateur(string prenom, string MDP)
        {
            String MDPEncode = EncodeMD5(MDP);
            Utilisateur utilisateur = new Utilisateur { Prenom = prenom, MDP = MDPEncode };
            bdd.Utilisateurs.Add(utilisateur);
            bdd.SaveChanges();

            return utilisateur.Id;
        }

        public Utilisateur Authentifier(string prenom, string MDP)
        {
            string testedMDP = EncodeMD5(MDP);
            Utilisateur TestedUser = bdd.Utilisateurs.FirstOrDefault(u => u.Prenom == prenom);

            if (TestedUser != null)
            {
                if (string.Compare(testedMDP, TestedUser.MDP) == 0)
                    return TestedUser;
            }
            return null;


        }



        public int CreerUnSondage()
        {
            Sondage NewSondage = new Sondage { Date = DateTime.Now};
            bdd.Sondages.Add(NewSondage);
            bdd.SaveChanges();
            return NewSondage.Id;
        }


        public bool ADejaVote(int IdSondage, string IdUtilisateurStr)
        {
            int id;
            if (int.TryParse(IdUtilisateurStr, out id))
            {
                Sondage sondage = bdd.Sondages.First(s => s.Id == IdSondage);
                if (sondage.Votes == null)
                    return false;
                return sondage.Votes.Any(v => v.Utilisateur != null && v.Utilisateur.Id == id);
            }
            return false;
        }

        public void AjouterVote(int idSondage, int idResto, int idUstilisateur)
        {
            Vote vote = new Vote
            {
                Resto = bdd.Restos.FirstOrDefault(r => r.Id == idResto),
                Utilisateur = bdd.Utilisateurs.First(u => u.Id == idUstilisateur)
            };

            Sondage sondage = bdd.Sondages.FirstOrDefault(s => s.Id == idSondage);
            if (sondage.Votes == null)
                sondage.Votes = new List<Vote>();
            sondage.Votes.Add(vote);
            bdd.SaveChanges();          
        }

        public List<Resultats> ObtenirLesResultats(int idSondage)
        {
            List<Resto> restaurants = ObtientTousLesRestaurants();
            List<Resultats> resultats = new List<Resultats>();

            Sondage sondage = bdd.Sondages.FirstOrDefault(i => i.Id == idSondage);
            foreach(IGrouping<int, Vote> grouping in sondage.Votes.GroupBy(v => v.Resto.Id))
            {
                int idRestaurant = grouping.Key;
                Resto resto = restaurants.First(r => r.Id == idRestaurant);
                int nombreDeVotes = grouping.Count();
                resultats.Add(new Resultats { Nom = resto.Nom, Telephone = resto.Telephone, NombreDeVotes = nombreDeVotes });
            }

            return resultats;



        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        private string EncodeMD5(string MDP)
        {
            string MDPSel = "ChoixResto" + MDP + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(MDPSel)));

        }
    }
}