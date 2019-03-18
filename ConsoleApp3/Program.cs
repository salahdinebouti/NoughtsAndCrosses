using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            int valeurOperandeGauche = DemanderOperandeALUtilisateur("Veuillez entrer l'opérande de gauche :");

            string operateur = DemanderOperateurALUtilisateur();

            int valeurOperandeDroite = DemanderOperandeALUtilisateur("Veuillez entrer l'opérande de droite :");

            if (valeurOperandeGauche != 0 || valeurOperandeDroite != 0)
            {
                switch (operateur)
                {
                    case "+":
                        {
                            int resultat = valeurOperandeGauche + valeurOperandeDroite;
                            Console.WriteLine("Addition : " + resultat);
                        }
                        break;
                    case "-":
                        {
                            int resultat = valeurOperandeGauche - valeurOperandeDroite;
                            Console.WriteLine("soustraction : " + resultat);
                        }
                        break;

                    case "*":
                        {
                            int resultat = valeurOperandeGauche * valeurOperandeDroite;
                            Console.WriteLine("Multiplication : " + resultat);
                        }
                        break;

                    case "/":
                        {
                            if (valeurOperandeDroite != 0)
                            {
                                int resultat = valeurOperandeGauche / valeurOperandeDroite;
                                Console.WriteLine("Division : " + resultat);
                            }
                            else
                            {
                                Console.WriteLine("On ne peut pas faire de division par zéro");
                            }
                        }
                        break;

                    case "^":
                        {
                            if (valeurOperandeDroite >= 0)
                            {
                                double resultatPuissance = Math.Pow(valeurOperandeGauche, valeurOperandeDroite);
                                Console.WriteLine("Puissance : " + resultatPuissance);
                            }
                            else
                            {
                                Console.WriteLine("On ne souhaite pas calculer des puissances inférieures à 0");
                            }
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Cet opérateur n'est pas connu");
                        }
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ces valeurs ne sont pas permises");
            }
            Console.ReadKey();
        }

        static int DemanderOperandeALUtilisateur(string message)
        {
            string saisieUtilisateur = DemanderSaisieUtilisateur(message);
            int valeurParsee = int.Parse(saisieUtilisateur);
            return valeurParsee;
        }

        static string DemanderOperateurALUtilisateur()
        {
            string operateur = DemanderSaisieUtilisateur("Veuillez saisir l'opérateur");
            return operateur;
        }

        static string DemanderSaisieUtilisateur(string message)
        {
            Console.WriteLine(message);
            string saisieUtilisateur = Console.ReadLine();
            return saisieUtilisateur;
        }
    }
}
