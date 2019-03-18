using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Morpion start!, choisir taille du morpion: ");
            string inputUser = Console.ReadLine();
            Morpion morpion = new Morpion(int.Parse(inputUser));

            while (true)
            {
                morpion.AfficherMorpion();
                int choixI = int.Parse(Console.ReadLine());
                int choixJ = int.Parse(Console.ReadLine());

                Tour tour = new Tour(choixI, choixJ);
                morpion.JouerUneCase(tour);
                morpion.DeterminerSymbolGagnant();
                Symbol? gagnant = morpion.DeterminerSymbolGagnant();

                if (gagnant == Symbol.x)
                {
                    morpion.AfficherMorpion();
                    Console.WriteLine("la croix gagne!");
                    Console.ReadKey(true);
                    return;
                }
                else if (gagnant == Symbol.o)
                {
                    morpion.AfficherMorpion();
                    Console.WriteLine("le rond gagne!");
                    Console.ReadKey(true);
                    return;
                }
            }
        }
    }

    public class Morpion
    {
        public Case[,] Cases { get; private set; }
        public Symbol SymboleDuJoueurQuiDoitJouer { get; private set; }

        public const int TAILLE_PAR_DEFAUT = 3;
        public const Symbol SYMBOL_PAR_DEFAUT = Symbol.x;

        public Morpion() : this(TAILLE_PAR_DEFAUT)
        {
        }
        public Morpion(int taille) : this(taille, SYMBOL_PAR_DEFAUT)
        {
        }
        public Morpion(int taille, Symbol SymboleDuJoueurQuiDoitJouer)
        {
            this.Cases = new Case[taille, taille];
            this.SymboleDuJoueurQuiDoitJouer = SymboleDuJoueurQuiDoitJouer;
            for (int i = 0; i < Cases.GetLength(0); i++)
            {
                for (int j = 0; j < Cases.GetLength(1); j++)
                {
                    Case @case = new Case();
                    this.Cases[i, j] = @case;
                }
            }
        }


        public void AfficherMorpion()
        {
            //Console.Clear();
            for (int i = 0; i < Cases.GetLength(0); i++)
            {
                for (int j = 0; j < Cases.GetLength(1); j++)
                {

                    if (this.Cases[i, j].SymbolCourant == Symbol.x)
                    {
                        Console.Write("|x");
                    }
                    else if (this.Cases[i, j].SymbolCourant == Symbol.o)
                    {
                        Console.Write("|o");
                    }
                    else
                        Console.Write("|_");
                }
                Console.WriteLine("|");
            }

            if (this.SymboleDuJoueurQuiDoitJouer == Symbol.x)
            {
                Console.Write("c'est au tour de x : ");
            }
            else if (this.SymboleDuJoueurQuiDoitJouer == Symbol.o)
            {
                Console.Write("c'est au tour de o : ");
            }

        }
        public void JouerUneCase(Tour tourAJouer)
        {
            //1ère étape : récupère dans la grille de morpion la cellule qui correspond au tour à jouer
            Case laCaseJouee = this.Cases[tourAJouer.indexLigneJouee, tourAJouer.indexColonneJouee];

            //2ème étape : sur cette case, je lui positionne le symbole du joueur courant
            Symbol symbolAPositionnerDansCaseJouee = this.SymboleDuJoueurQuiDoitJouer;
            laCaseJouee.PositionnerSymbol(symbolAPositionnerDansCaseJouee);

            //3ème étape : j'inverse le joueur courant
            Symbol leProchainSymbolQuiDoitJouer =
                this.SymboleDuJoueurQuiDoitJouer == Symbol.x ? Symbol.o : Symbol.x;
            this.SymboleDuJoueurQuiDoitJouer = leProchainSymbolQuiDoitJouer;
        }

        public Symbol? DeterminerSymbolGagnant()
        {

            //Pour chaques lignes
            //  si toutes les cases de cette ligne ont le même symboles 
            //  et que ce symbole n'est pas "vide"
            //      alors le symbol gagnant est le symbol de la 1ère case de la ligne
            //      je retourne le symbole gagnant
            for (int indexLigne = 0; indexLigne < Cases.GetLength(0); indexLigne++)
            {
                if (this.Cases[indexLigne, 0].SymbolCourant != null &&
                    this.Cases[indexLigne, 0].SymbolCourant == this.Cases[indexLigne, 1].SymbolCourant
                    && this.Cases[indexLigne, 0].SymbolCourant == this.Cases[indexLigne, 2].SymbolCourant)
                {
                    return this.Cases[indexLigne, 0].SymbolCourant;
                }
            }

            //Pour chaques colonnes
            //  si toutes les cases de cette colonne ont le même symboles
            //  et que ce symbole n'est pas "vide"
            //      alors le symbole gagnant est le symbole de la 1ère case de la colonne
            //      je retourne le symbole gagnant

            for (int indexColonne = 0; indexColonne < 3; indexColonne++)
            {
                if (this.Cases[0, indexColonne].SymbolCourant != null &&
                    this.Cases[0, indexColonne].SymbolCourant == this.Cases[1, indexColonne].SymbolCourant
                    && this.Cases[0, indexColonne].SymbolCourant == this.Cases[2, indexColonne].SymbolCourant)
                {
                    return this.Cases[0, indexColonne].SymbolCourant;
                }
            }

            //Pour les 2 diagonales
            //  si toutes les cases de cette diagonale ont le même symboles
            //  et que ce symbole n'est pas "vide"
            //      alors le symbole gagnant est le symbole de la 1ère case de la diagonale
            //      je retourne le symbole gagnant

            if (this.Cases[0, 0].SymbolCourant != null
                && this.Cases[0, 0].SymbolCourant == this.Cases[1, 1].SymbolCourant
                && this.Cases[0, 0].SymbolCourant == this.Cases[2, 2].SymbolCourant)
            {
                return this.Cases[0, 0].SymbolCourant;
            }


            if (this.Cases[2, 0].SymbolCourant != null
                && this.Cases[2, 0].SymbolCourant == this.Cases[1, 1].SymbolCourant
                && this.Cases[2, 0].SymbolCourant == this.Cases[0, 2].SymbolCourant)
            {
                return this.Cases[0, 0].SymbolCourant;
            }

            //Sinon
            //  Il n'y a aucun gagnant
            return null;
        }
    }

    public class Case
    {
        public Symbol? SymbolCourant { get; private set; }

        public Case() : this(null)
        {
        }

        public Case(Symbol? symbolCourant)
        {
            SymbolCourant = symbolCourant;
        }

        public void PositionnerSymbol(Symbol nouveauSymbol)
        {
            if (this.SymbolCourant == null)
            {
                this.SymbolCourant = nouveauSymbol;
            }
        }
    }
    public enum Symbol
    {
        x,
        o,
    }
    public class Tour
    {
        public int indexLigneJouee { get; private set; }
        public int indexColonneJouee { get; private set; }
        public Tour(int indexLigneJouee, int indexColonneJouee)
        {
            this.indexColonneJouee = indexColonneJouee;
            this.indexLigneJouee = indexLigneJouee;
        }

    }

}
