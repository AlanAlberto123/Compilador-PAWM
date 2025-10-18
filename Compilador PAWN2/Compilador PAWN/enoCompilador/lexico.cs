public class Lexico
{
    Nodo? cabeza = null;
    Nodo? p = null;
    int estado = 0, columna, valorMT, numRenglon = 1;
    int caracter = 0;
    string lexema = "";
    bool errorEncontrado = false;

    string archivo = @"C:\Users\alana\Downloads\Compilador PAWN2\Compilador PAWN\enoCompilador\CasoValido1.txt";

    int[,] tabla = new int[17, 31] { 
         //   L    D    _    .    "    '   +    -    *     /   %    =    !    >    <    &    |    (    )    [    ]    {    }    ,    ;    :   oc   eb   tab   nl   rt
/*00*/    {   1,   2,   1, 500,   5,   6, 104, 105, 106,   7, 108,  10,  11,  12,  13,  14,  15, 118, 119, 120, 121, 122, 123, 124, 125, 126, 500,   0,   0,   0,    0},
/*01*/    {   1,   1,   1, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,  100},
/*02*/    { 101,   2, 101,   3, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 101, 111, 111, 111,  101},
/*03*/    { 501,   4, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501, 501,  501},
/*04*/    { 102,   4, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102, 102,  102},
/*05*/    {   5,   5,   5,   5, 103,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5,   5, 502,    5},
/*06*/    {   6,   6,   6,   6,   6,   0,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6,   6, 503, 503, 503, 503,  503},
/*07*/    { 107, 107, 107, 107, 107, 107, 107, 107,   8,  16, 107, 107, 107, 107, 107, 107, 107, 107, 107, 107, 107, 107, 107, 107, 107, 107, 107, 107, 107, 107,  107},
/*08*/    {   8,   8,   8,   8,   8,   8,   8,   8,   9,   8,   8,   8,   8,   8,   8,   8,   8,   8,   8,   8,   8,   8,   8,   8,   8,   8,   8,   8,   8,   8,    8},
/*09*/    { 504, 504, 504, 504, 504, 504, 504, 504, 504,   0, 504, 504, 504, 504, 504, 504, 504, 504, 504, 504, 504, 504, 504, 504, 504, 504, 504, 504, 504, 504,  504},
/*10*/    { 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 109, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127,  127},
/*11*/    { 117, 117, 117, 117, 117, 117, 117, 117, 117, 117, 117, 110, 117, 117, 117, 117, 117, 117, 117, 117, 117, 117, 117, 117, 117, 117, 117, 117, 117, 117,  117},
/*12*/    { 111, 111, 111, 111, 111, 111, 111, 111, 111, 111, 111, 113, 111, 111, 111, 111, 111, 111, 111, 111, 111, 111, 111, 111, 111, 111, 111, 111, 111, 111,  111},
/*13*/    { 112, 112, 112, 112, 112, 112, 112, 112, 112, 112, 112, 114, 112, 112, 112, 112, 112, 112, 112, 112, 112, 112, 112, 112, 112, 112, 112, 112, 112, 112,  112},
/*14*/    { 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128, 128,  128},
/*15*/    { 506, 506, 506, 506, 506, 506, 506, 506, 506, 506, 506, 506, 506, 506, 506, 506, 116, 506, 506, 506, 506, 506, 506, 506, 506, 506, 506, 506, 506, 506,  506},
/*16*/    {  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,  16,   0,   16},
};

    string[,] palabrasReservadas = new string[13, 2] {
        //            0           1
        /*  1 */ { "main",      "201" },
        /*  2 */ { "new",       "202" },
        /*  3 */ { "int",       "203" },
        /*  4 */ { "float",     "204" },
        /*  5 */ { "string",    "205" },
        /*  6 */ { "bool",      "206" },
        /*  7 */ { "return",    "207" },
        /*  8 */ { "if",        "208" },
        /*  9 */ { "else",      "209" },
        /* 10 */ { "while",     "210" },
        /* 11 */ { "do",        "211" },
        /* 12 */ { "scanf",     "212" },
        /* 13 */ { "print",     "213" }
    };

    string[,] errores = new string[7, 2] {  
    //             0                                 1
    /* 1 */ { "Se espera un dígito",              "501" },  
    /* 0 */ { "Caracter no valido",               "500" },  
    /* 2 */ { "Se espera una comillas",           "502" },  
    /* 3 */ { "Se espera ' ",                     "503" },
    /* 4 */ { "Se esperaba fin de comentario",    "504" },    
    /* 7 */ { "Se esperaba &",                    "505" },
    /* 8 */ { "Se esperaba |",                    "506" }
    };


    public Lexico()
    {

        Console.WriteLine("Leyendo archivo: " + archivo);

        if (!File.Exists(archivo))
        {
            Console.WriteLine("ERROR: El archivo no existe en la ruta: " + Path.GetFullPath(archivo));
            errorEncontrado = true;
            return;
        }
        FileStream fs = null;
        BinaryReader lector = null;

        try
        {
            fs = new FileStream(archivo, FileMode.Open, FileAccess.Read);
            lector = new BinaryReader(fs);

            while ((caracter = lector.Read()) != -1)
            {
                char charActual = (char)caracter;

                if (char.IsLetter(charActual))
                {
                    columna = 0;
                }
                else if (char.IsDigit(charActual))
                {
                    columna = 1;
                }
                else
                {


                    switch (charActual)
                    {
                        case ' ': columna = 28; break;
                        case '\t': columna = 28; break;
                        case '_': columna = 2; break;
                        case '.': columna = 3; break;
                        case '"': columna = 4; break;
                        case '\'': columna = 5; break;
                        case '+': columna = 6; break;
                        case '-': columna = 7; break;
                        case '*': columna = 8; break;
                        case '/': columna = 9; break;
                        case '%': columna = 10; break;
                        case '=': columna = 11; break;
                        case '!': columna = 12; break;
                        case '>': columna = 13; break;
                        case '<': columna = 14; break;
                        case '&': columna = 15; break;
                        case '|': columna = 16; break;
                        case '(': columna = 17; break;
                        case ')': columna = 18; break;
                        case '[': columna = 19; break;
                        case ']': columna = 20; break;
                        case '{': columna = 21; break;
                        case '}': columna = 22; break;
                        case ',': columna = 23; break;
                        case ';': columna = 24; break;
                        case ':': columna = 25; break;
                        case (char)13: columna = 30; break;
                        case '\n': columna = 29; numRenglon++; break;
                        default: columna = 26; break;
                    }
                }

                valorMT = tabla[estado, columna];


                if (valorMT < 100)
                {
                    estado = valorMT;

                    if (estado == 0)
                    {
                        lexema = "";
                    }
                    else
                    {
                        lexema += charActual;
                    }


                }
                else if (valorMT >= 100 && valorMT < 500)
                {
                    if (valorMT == 100)
                    {
                        lexema = lexema.Trim();
                        ValidarPalabrasReservadas();
                    }

                    if ((estado == 1 || estado == 2) && !char.IsLetter(charActual))
                    {
                        fs.Position -= 1;
                        lexema = lexema.Trim();
                        AgregarNodo(lexema, valorMT, numRenglon);
                        estado = 0;
                        lexema = "";
                        fs.Position += 1;
                        valorMT = tabla[estado, columna];

                    }

                    if ((valorMT >= 100 && valorMT <= 199) || valorMT >= 200)
                    {
                        

                        lexema = lexema.Trim();
                        if (lector != null && fs != null && fs.Position > 0)
                        {
                            fs.Seek(0, SeekOrigin.Current);
                            lexema += charActual;
                        }
                        AgregarNodo(lexema, valorMT, numRenglon);
                        estado = 0;
                        lexema = "";
                    }
                    else
                    {
                        lexema += charActual;
                    }
                }
                else
                {
                    if (columna == 29)
                    {
                        numRenglon--;

                    }
                    MensajeError(valorMT, numRenglon, lexema + charActual);
                    break;
                }
                Console.WriteLine($"Procesando: '{charActual}' (Estado: {estado})");
            }
            if (estado != 0 && lexema != "")
            {
                if (estado == 1)
                {
                    valorMT = 100;
                    ValidarPalabrasReservadas();
                    AgregarNodo(lexema, valorMT, numRenglon);
                }
                else if (estado == 2 || estado == 4)
                {
                    AgregarNodo(lexema, valorMT, numRenglon);
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: El archivo '{archivo}' no se encontró.");
            errorEncontrado = true;
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error al leer el archivo '{archivo}': {ex.Message}");
            errorEncontrado = true;
        }
        finally
        {
            lector?.Close();
            fs?.Close();
        }
        ImprimirNodos();
    }
    private void ValidarPalabrasReservadas()
    {
        for (int i = 0; i < palabrasReservadas.GetLength(0); i++)
        {
            if (lexema == palabrasReservadas[i, 0])
            {
                valorMT = int.Parse(palabrasReservadas[i, 1]);
                return;
            }
        }
    }

    private void AgregarNodo(string lexema, int token, int renglon)
    {
        Nodo nuevo = new Nodo(lexema, token, renglon);
        if (cabeza == null)
        {
            cabeza = nuevo;
        }
        else
        {
            Nodo temp = cabeza;
            while (temp.sig != null)
            {
                temp = temp.sig;
            }
            temp.sig = nuevo;
        }
    }

    private void MensajeError(int codigoError, int linea, string lexemaError)
    {
        errorEncontrado = true;
        string mensaje = $"Error en línea {linea}: ";

        for (int i = 0; i < errores.GetLength(0); i++)
        {
            if (int.Parse(errores[i, 1]) == codigoError)
            {
                mensaje += errores[i, 0];
                break;
            }
        }

        Console.WriteLine($"{mensaje} -> '{lexemaError}'");
    }

    private void ImprimirNodos()
    {
        if (cabeza == null)
        {
            Console.WriteLine("No se encontraron tokens");
            return;
        }

        Console.WriteLine("TOKENS ENCONTRADOS:");
        Console.WriteLine("Lexema\tToken\tLínea");
        Console.WriteLine("---------------------");

        Nodo? temp = cabeza;
        while (temp != null)
        {
            Console.WriteLine($"{temp.lexema}\t{temp.token}\t{temp.renglon}");
            temp = temp.sig;
        }
    }

    public bool ErrorEncontrado
    {
        get { return errorEncontrado; }
    }

    public Nodo? listaNodos
    {
        get { return cabeza; }
    }

}

