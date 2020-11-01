//Stwórz aplikację, która pomoże lokalizować książki w bibliotece.
//Stwórz klasę Książka (Ksiazka), która będzie miała pola/właściwości Tytuł, Autor a także dane służące lokalizacji - numer regału, numer półki i numer miejsca na półce.
//Stwórz trójwymiarową tablicę prostokątną obiektów typu Książka. Pierwszy wymiar odwzorowuje Regał, drugi Półkę a trzeci Miejsce. Przyjmij, że w bibliotece są 3 regały po 6 półek po 10 miejsc każda.
//Zapełnij tablicę książkami, wszystkie książki mogą nazywać się tak samo. Każda książka powinna posiadać poprawne informacje o swojej lokalizacji. Zmień tytuł i autora jednej dowolnej książki.
//Pozwól użytkownikowy wpisać ciąg znaków. Spróbuj znaleźć książkę, której tytuł lub autor zawierają podany przez użytkownika ciąg znaków (wystarczy że choć wycinek jest zgodny).
//Jeśli poszukiwana książka istnieje, wypisz użytkownikowi gdzie się znajduje. Przetestuj na zmienionej książce.

using System;


namespace P3_LAB4_zadanie
{
    class main
    {
        static void Main(string[] args)
        {
            Ksiazka[,,] biblioteka = new Ksiazka[3, 6, 10];     //tablica prostokątna typu ksiazka z polami o wartości null
            SetLocalisation(biblioteka);                        //ustala lokalizacje kazdej ksiazki ktora jest zapisana w tablicy (pole klasy)
            GetLocalisation(biblioteka);

            try
            {
                SetNewBook(2, 0, 0, "\"1984\"", "George Orwell", biblioteka);
                SetNewBook(1, 3, 6, "\"Krótka historia czasu\"", "Stephen Hawking", biblioteka);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Złapano wyjątek");
            }
            GetLocalisation(biblioteka);
            SearchBook("Geo", biblioteka);
            SearchBook("haw", biblioteka);

        }

        static void SetLocalisation(Ksiazka[,,] biblioteka)
        {
            for (int i = 0; i < biblioteka.GetLength(0); i++)
            {
                for (int j = 0; j < biblioteka.GetLength(1); j++)
                {
                    for (int k = 0; k < biblioteka.GetLength(2); k++)
                    {
                        biblioteka[i, j, k] = new Ksiazka();            // przypisanie obiektu do indeksu (wywołanie konstruktora)
                        biblioteka[i, j, k].localisation = new int[] { i, j, k }; // wpisanie lokazlicaji do tablicy z aktualnej iteracji
                    }
                }
            }
        }

        static void GetLocalisation(Ksiazka[,,] biblioteka)
        {
            for (int i = 0; i < biblioteka.GetLength(0); i++)
            {
                for (int j = 0; j < biblioteka.GetLength(1); j++)
                {
                    for (int k = 0; k < biblioteka.GetLength(2); k++)
                    {
                        Console.WriteLine($"Pod adresem - półka: {i + 1} ({biblioteka[i, j, k].localisation[0]+1}), regał: {j + 1} ({biblioteka[i, j, k].localisation[1] + 1}), pozycja: {k + 1} ({biblioteka[i, j, k].localisation[2] + 1})znajduje się: {biblioteka[i, j, k].Author}  {biblioteka[i, j, k].Title} ");
                    }
                }
            }
        }
        static void SetNewBook(int regal, int polka, int pozycja, string author, string title, Ksiazka[,,] biblioteka)
        {
            if (regal < 3 && polka < 6 && pozycja < 10)
            {
                author = author.ToUpper();
                title = title.ToUpper();
                int[] loc = biblioteka[regal, polka, pozycja].localisation;
                biblioteka[regal, polka, pozycja] = new Ksiazka(author, title);
                biblioteka[regal, polka, pozycja].localisation = loc;             // WAŻNE!!! przypisanie tej samej pozycji którą miała poprzednia ksiazka do nowej
            }
            else
            {
                Console.WriteLine("Podaj poprawną nową lokalizacje!");
                throw new ArgumentOutOfRangeException();

            }
        }
        static void SearchBook(string word, Ksiazka[,,] biblioteka)
        {
            word = word.ToUpper();
            for (int i = 0; i < biblioteka.GetLength(0); i++)
            {
                for (int j = 0; j < biblioteka.GetLength(1); j++)
                {
                    for (int k = 0; k < biblioteka.GetLength(2); k++)
                    {
                        if (biblioteka[i, j, k].Author.Contains(word) || biblioteka[i, j, k].Title.Contains(word))
                        {
                            Console.WriteLine($"Lokalizacja - półka: {i + 1}, regał: {j + 1}, pozycja: {k + 1}");
                            Console.WriteLine(biblioteka[i, j, k].Author + " " + biblioteka[i, j, k].Title);
                        }
                    }
                }
            }
        }

        class Ksiazka
        {
            private string _title;
            private string _author;
            public int[] localisation = new int[3];// index 0 - regał, index 1 - półka, index 2 - miejsce
            public Ksiazka()
            {
                Title = "Nieznany tytuł";
                Author = "Nieznany autor";
            }
            public Ksiazka(string title, string author)
            {
                _title = title;
                _author = author;
            }

            public string Title
            {
                get
                {
                    return _title;
                }
                set
                {
                    _title = value.ToUpper();
                }
            }
            public string Author
            {
                get
                {
                    return _author;
                }
                set
                {
                    _author = value.ToUpper();
                }
            }
        }
    }
}