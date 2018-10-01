using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixResto.Models
{
    public interface IDal : IDisposable
    {
        void CreerRestaurant(string nom, string telephone);
        void ModifierRestaurant(int id, string nom, string telephone);
        bool RestaurantExiste(string nom);
        Utilisateur ObtenirUtilisateur(int id);
        Utilisateur ObtenirUtilisateur(string IdStr);
        int AjouterUtilisateur(string prenom, string MDP);
        Utilisateur Authentifier(string prenom, string MDP);
        int CreerUnSondage();
        bool ADejaVote(int IdSondage, string IdUtilisateurStr);
        void AjouterVote(int idSondage, int idResto, int idUstilisateur);
        List<Resultats> ObtenirLesResultats(int idSondage);
        List<Resto> ObtientTousLesRestaurants();
    }


}